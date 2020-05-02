using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Labirintus
{
   class GameSetting : SettingBase
   {
      internal string levelName
      {
         get => getString ();
         set => Set ( value );
      }
    
      internal GameSetting ( string file ) : base ( file ) { }
   }
   //---------------------------------------------------------
   class ProgramSetting : SettingBase
   {
      internal double Left
      {
         get => getDoubleOrNan ();
         set => Set ( value );
      }
      internal double Top
      {
         get => getDoubleOrNan ();
         set => Set ( value );
      }
      internal double Width
      {
         get => getDoubleOrNan ();
         set => Set ( value );
      }
      internal double Height
      {
         get => getDoubleOrNan ();
         set => Set ( value );
      }

      internal ProgramSetting ( string file ) : base ( file ) { }
   }
   //-----------------------------------------------------------
   abstract class SettingBase
   {
      XDocument xml;
      string savePath;
      internal SettingBase ( string file )
      {
         savePath = file;

         try { xml = XDocument.Load ( file ); }
         catch
         {
            try
            {
               File.Delete ( file );
               xml = XDocument.Parse ( "<root></root>" );
               xml.Save ( file );
            }
            catch { return; }
         }
      }
      string _get ( string prop )
      {
         XElement elem = xml?.Root?.Element ( prop );
         return elem?.Value;
      }
      protected string getString ( [CallerMemberName] string prop = null )
      {
         return _get ( prop );
      }
      protected int getInt ( [CallerMemberName] string prop = null )
      {
         string value = _get ( prop );
         if ( int.TryParse ( value, out int dig ) )
            return dig;
         return 0;
      }

      double? _getdouble ( [CallerMemberName] string prop = null )
      {
         string value = _get ( prop );
         if ( double.TryParse ( value, out double dig ) )
            return dig;
         return null;
      }
      protected double getDoubleOrZero ( [CallerMemberName] string prop = null )
      {
         double? value = _getdouble ( prop );
         return value == null ? 0.0 : value.Value;
      }
      protected double getDoubleOrNan ( [CallerMemberName] string prop = null )
      {
         double? value = _getdouble ( prop );
         return value == null ? double.NaN : value.Value;
      }
      protected void Set ( object value, [CallerMemberName] string prop = null )
      {
         if ( string.IsNullOrWhiteSpace ( prop ) )
            return;

         XElement elem = xml.Root.Element ( prop );
         if ( elem == null )
         {
            elem = new XElement ( prop );
            xml.Root.Add ( elem );
         }
         elem.Value = value?.ToString ();
         xml.Save ( savePath );
      }
   }
}
