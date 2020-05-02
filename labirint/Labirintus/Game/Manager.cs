using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace Labirintus
{
   class GameManager : BaseNotify
   {
      string gameDir { get; set; }
      public List<Level> levelList { get; set; } = new List<Level> ();
      GameSetting setting;
      public SkinsCollection Skins { get; set; }

      private Game _game;
      public Game game
      {
         get { return _game; }
         set { _game = value; SetProperty (); }
      }

      private bool _ispaused;
      public bool IsPaused
      {
         get { return _ispaused; }
         set { _ispaused = value; SetProperty (); }
      }

      private bool _isloading;
      public bool IsLoading
      {
         get { return _isloading; }
         set { _isloading = value; SetProperty (); }
      }

      private Skin _currentskin;
      public Skin currentSkin
      {
         get { return _currentskin; }
         set { _currentskin = value; SetProperty (); }
      }

      Level currentLevel { get; set; }

      public GameManager () { }//  XAML
      internal GameManager ( string dir )
      {
         setting = new GameSetting ( dir + "\\gamesetting.xml" );

         //загрузка и сортировка по имени уровней из папки levels
         try { levelList = Directory.GetFiles ( dir + "\\levels" ).Select ( file => new Level ( file ) { selectionChanged = levelChanged } ).ToList (); } catch { }
         if ( levelList.Count == 0 )
            return;
         levelList.Sort (); // сортировка по имени!!! Имена уровней должны возрастать

         //загрузка Skins
         Skins = new SkinsCollection ( dir + "\\Skins", skinChanged );

         //поиск и загрузка последнего сохраненного уровня по имени. 
         string savedLevelName = setting.levelName;
         Level savedLevel = levelList.FirstOrDefault ( lev => lev.shortName == savedLevelName );
         if ( savedLevel == null )
            savedLevel = levelList [ 0 ];

         //Загрузка активируется через savedLevel.IsSelected, которое вызывает событие levelChanged 
         savedLevel.IsSelected = true;
      }
        void levelChanged ( Level level )
      {
         if ( level != currentLevel )
         {
            StartGame ( level );
            IsPaused = true;  
                             
         }
      }
      void skinChanged ( Skin skin )
      {
         if ( skin != null && skin != currentSkin )
         {
            currentSkin = skin;
            game.ApplySkin ( skin );
            IsPaused = true;
         }
      }
      void StartGame ( Level level )
      {
         game = new Game ( level );
         game.WinGame = WinGameEvent;

         currentSkin = Skins.getNextSkin ();
         game.ApplySkin ( currentSkin );
         if ( currentSkin != null ) currentSkin.IsSelected = true;

         setting.levelName = level.shortName; 
         currentLevel = level;
         level.IsSelected = true;   
         IsPaused = false;    
      }

      // когда игрок прошел уровень
      void WinGameEvent ()
      {
         IsPaused = true;
         // поиск следующего уровня
         Level nextLevel = null;
         int currentIndex = levelList.IndexOf ( currentLevel );
         if ( currentIndex >= 0 && currentIndex < levelList.Count - 1 )
         {          
            nextLevel = levelList [ currentIndex + 1 ];
         }

         Task.Run ( () =>
         {
            if ( nextLevel == null )
               Thread.Sleep ( 100 ); 
            else
            {
               IsLoading = true; 
               Thread.Sleep ( 1000 );
               IsLoading = false;
            }
         } ).GetAwaiter ().OnCompleted ( () =>
         {           

            if ( nextLevel == null ) 
            {

               if ( MessageBox.Show ( "Все уровни пройдены!\r\nДа - начать сначала\r\nНет - выбрать уровень", "End of game", MessageBoxButton.YesNo ) == MessageBoxResult.Yes )
               {
                  StartGame ( levelList [ 0 ] );
               }
               else
               {
                  currentLevel.IsSelected = false; 
                  currentLevel = null;
               }
            }
            else
            {
               StartGame ( nextLevel );
            }
         } );
      }

      //эти Key только для Game, передаются keyTimer
      internal void moveKeyEvent ( Key key, int interval )
      {
         if ( IsPaused || currentLevel == null )
            return;
         game?.moveKeyEvent ( key, interval );
      }

      //эти Key для Manager
      internal void keyPress ( Key key )
      {
         switch ( key )
         {
            case Key.Escape:
               if ( currentLevel != null )
               {
                  if ( MessageBox.Show ( "Начать уровень сначала?", "", MessageBoxButton.YesNo ) == MessageBoxResult.Yes )
                     StartGame ( currentLevel );
               }
               else
                  MessageBox.Show ( "Выберите любой уровень из списка" );

               break;
         }
      }
   }
   //--------------------------------------------------------

   class Level : BaseNotify, IComparable<Level> // для показа уровня в списке и его выбора
   {
      public string fullPath { get; set; }
      internal Action<Level> selectionChanged;

      private bool _isselected;
      public bool IsSelected
      {
         get { return _isselected; }
         set { _isselected = value; SetProperty (); if ( value ) selectionChanged?.Invoke ( this ); }
      }
      public string shortName => Path.GetFileNameWithoutExtension ( fullPath );
      internal Level ( string path )
      {
         fullPath = path;
      }

      public int CompareTo ( Level other )
      {
         return string.Compare ( shortName, other.shortName, true );
      }
   }


}
