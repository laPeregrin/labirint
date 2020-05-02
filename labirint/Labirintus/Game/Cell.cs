using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Labirintus
{
   abstract class Cell : BaseNotify
   {
      public int Row, Col;
      public virtual bool IsTransient { get; set; }// означает - можно ли проходить игроку сквозь эту клетку

      protected virtual string file { get; set; } = "images/empty.png";
      public virtual string File
      {
         get => "pack://siteOfOrigin:,,,/" + file;    //можно любые файлы из папки где лежит .exe, без включения их в проект
         set { if ( !string.IsNullOrWhiteSpace ( value ) ) file = value; SetProperty (); }
      }

      protected virtual Brush bkg { get; set; }
      public virtual Brush Bkg { get => bkg; set { bkg = value; SetProperty (); } }
   }
   class Space : Cell
   {
      public override bool IsTransient => true;
   }

   class Player : Cell
   {
      protected override string file { get; set; } = "images/player.png";
      private int _step;
     public int Step { get => _step; set {_step= value; SetProperty(); } }
   }
   class Wall : Cell
   {
      protected override string file { get; set; } = "images/stone.jpg.jpg";
      protected override Brush bkg => Brushes.Black;
   }
   class Exit : Cell
   {
      public override bool IsTransient => true;
      protected override string file { get; set; } = "images/exite.png";
   }
   class BaseNotify : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      protected void SetProperty ( [CallerMemberName] string prop = null )
      {
         PropertyChanged?.Invoke ( this, new PropertyChangedEventArgs ( prop ) );
      }
   }
}
