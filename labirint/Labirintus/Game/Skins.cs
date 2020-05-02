using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirintus
{
   class SkinsCollection
   {
      string skinDir;
      int currentIndex = 0;
      public List<Skin> skinList { get; set; } = new List<Skin> ();

      internal SkinsCollection ( string dir, Action<Skin> changedEvent )
      {
         skinDir = dir;
         try { skinList = Directory.GetFiles ( skinDir ).Select ( file => new Skin ( file ) ).ToList (); } catch { }
         // находим файл default.skin
         Skin defaultSkin = skinList.FirstOrDefault ( s => s.IsDefault );
         //удаляем пустые и default(он будет у всех)
         skinList.RemoveAll ( s => ( !s.IsOk || s.IsDefault ) );
         //записываем default всем и событие changed, Если не найдет значение в текущем, то будет искать в default
         skinList.ForEach ( s =>{ s.defaultSkin = defaultSkin; s.selectionChanged = changedEvent; } );
      }

      internal Skin getCurrentSkin ()
      {
         if ( skinList.Count > 0 )
            if ( currentIndex < skinList.Count )
               return skinList [ currentIndex ];
         return null;
      }
      internal Skin getNextSkin ()
      {
         if ( skinList.Count > 0 )
         {
            int current = currentIndex;

            currentIndex++;
            if ( currentIndex >= skinList.Count )
               currentIndex = 0;

            return skinList [ current ];
         }
         return null;
      }
   }
   class Skin : BaseNotify
   {
      internal bool IsOk;
      internal bool IsDefault;
      internal Skin defaultSkin; // чтоб подставлять из default.skin если нет в текущем
      Dictionary<string, string> datalist = new Dictionary<string, string> ( StringComparer.OrdinalIgnoreCase );
      public string shortName { get; set; }
      internal Action<Skin> selectionChanged;
      internal string getValue ( string key )
      {
         if ( datalist.TryGetValue ( key, out string value ) )
            return value;

         // если не найден, то искать в default
         // если и там нет, то останется, то которое записано в коде класса
         return defaultSkin?.getValue ( key );
      }
      internal Skin ( string file )
      {
         try
         {
            shortName = Path.GetFileNameWithoutExtension ( file );
            if ( shortName == "default" )
               IsDefault = true;

            string [] lines = File.ReadAllLines ( file );
            foreach ( string line in lines )
            {
               string [] data = line.Split ( new char [] { '=' }, StringSplitOptions.RemoveEmptyEntries );
               if ( data.Count () == 2 )
                  datalist.Add ( data [ 0 ].Trim (), data [ 1 ].Trim () );
            }
            IsOk = datalist.Count > 0;
         }
         catch { }
      }

     
      public string Bkg => "pack://siteOfOrigin:,,,/" + getValue ( "bkg" ); // в ItemsControl.Background.ImageSource
      private bool _isselected;
      public bool IsSelected
      {
         get { return _isselected; }
         set { _isselected = value; if ( value ) selectionChanged?.Invoke ( this ); SetProperty (); }
      }


   }
}
