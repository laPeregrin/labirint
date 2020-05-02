using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Labirintus
{
   class Game
   {
      public ObservableCollection<ObservableCollection<Cell>> karta { get; set; }

      int Height, Width;
      public Player player { get; set; }
      internal Action WinGame { get; set; }

      internal Game ( Level level )
      {
         parserBmp ( level.fullPath );
      }

      internal void moveKeyEvent ( Key key, int interval )
      {
         int dx = 0, dy = 0;
         switch ( key )
         {
            case Key.Up: dy = -1; break;
            case Key.Right: dx = 1; break;
            case Key.Down: dy = 1; break;
            case Key.Left: dx = -1; break;
            default: return;
         }

         int Y = player.Row, X = player.Col;
         int nextY = Y + dy, nextX = X + dx;

         if ( nextY < 0 || nextY >= Height ) return;
         if ( nextX < 0 || nextX >= Width ) return;

         Cell target = karta [ nextY ] [ nextX ];

         if ( target is Exit )
            WinGame?.Invoke (); // send message to Manager

         if ( target.IsTransient )
         {
            karta [ Y ] [ X ] = new Space () { Row = Y, Col = X };
            karta [ nextY ] [ nextX ] = player;
            player.Row = nextY;
            player.Col = nextX;
            player.Step++;
         }
      }
      void parserBmp ( string file )
      {
         karta = new ObservableCollection<ObservableCollection<Cell>> ();

         try
         {
            BitmapImage bmp = new BitmapImage ( new Uri ( file ) );

            Width = bmp.PixelWidth;
            Height = bmp.PixelHeight;

            int stride = Width * 4;
            int size = Height * stride;
            byte [] pixels = new byte [ size ];
            bmp.CopyPixels ( pixels, stride, 0 );

            for ( int y = 0; y < Height; y++ )
            {
               karta.Add ( new ObservableCollection<Cell> () );

               for ( int x = 0; x < Width; x++ )
               {
                  int index = y * stride + 4 * x;
                  byte blue = pixels [ index ];
                  byte green = pixels [ index + 1 ];
                  byte red = pixels [ index + 2 ];
                  Cell item = null;

                  if ( red == 0 && green == 0 && blue == 0 ) // black// Wall
                     item = new Wall ();
                  else if ( red == 0 && green == 255 && blue == 0 ) //green// Exit
                     item = new Exit ();
                  else if ( red == 255 && green == 0 && blue == 0 ) // red // Player
                     item = player = new Player ();
                  else                                      
                     item = new Space ();

                  item.Row = y;
                  item.Col = x;
                  karta [ y ].Add ( item );
               }
            }

            if ( player == null && Height > 1 && Width > 1 )
            {
               player = new Player () { Row = 1, Col = 1 };
               karta [ 1 ] [ 1 ] = player;
            }
         }
         catch { }
      }

      internal void ApplySkin ( Skin skin )
      {
         if ( skin == null )
            return;

         foreach ( IEnumerable<Cell> row in karta )
            foreach ( Cell cell in row )
            {
               string name = cell.GetType ().Name;              
               cell.File = skin.getValue ( name );
            }
      }
   }
}
