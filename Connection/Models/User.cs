using System;
using System.Collections.Generic;
using System.Text;

namespace FamsGames.Model
{
    public class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
    }
    public class Score
    {
        public int IdGame { get; set; }
        public int IdUser { get; set; }
        public int Points { get; set; }
        public int Tries { get; set; }
    }

    public class Trivial
    {
        public string Pregunta { get; set; }
        public string[] Respuestas { get; set; }
        public string Correccion { get; set; }
        public bool Descartada { get; set; }
        public int IdPregunta { get; set; }
    }
}
