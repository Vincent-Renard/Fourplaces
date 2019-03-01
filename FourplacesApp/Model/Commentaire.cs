using System;
namespace FourplacesApp.Model
{
    public class Commentaire
    {
        public String Autor {get;private set;}
        public String Comment { get; private set; }
        public DateTime DateComment { get; private set; }

        public Commentaire(String who,string what)

        {
            this.Autor = who;
            this.Comment = what;
            this.DateComment = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("[Commentaire: Autor={0}, Comment={1}, DateComment={2}]", Autor, Comment, DateComment);
        }
    }
}
