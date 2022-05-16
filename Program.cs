using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.Beep(2000, 1000, 1);

            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("Academia de programadores");
            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());
            var evalList = reporteador.GetListaEvaluaciones();
            var listaAsg = reporteador.GetListaAsignaturas();
            var listaEvalXAsig = reporteador.GetDicEvaluaXAsig();
            var listaPromXAsig = reporteador.GetPromeAlumnPorAsignatura();

            Printer.WriteTitle("Ingrese evalucación");
            var newEval = new Evaluación();
            string nombre, notastring;
            float nota;

            WriteLine("Nombre de la evaluación?");
            Printer.PresioneENTER();
            nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Printer.WriteTitle("El nombre no debe de estar vacío");
                WriteLine("Cerrando");
            }
            else
            {
                newEval.Nombre = nombre.ToLower();
                WriteLine("Guardado");
            }


            WriteLine("Ingrese una nota");
            Printer.PresioneENTER();
            notastring = Console.ReadLine();


            try
            {
                newEval.Nota = float.Parse(notastring);
                if (newEval.Nota < 0 || newEval.Nota > 5)
                {
                    throw new ArgumentOutOfRangeException("La nota debe estar en el rango 0 a 5");
                }
                WriteLine("Registrado exitosamente");
                return;
            }
            catch (ArgumentOutOfRangeException arge)
            {
                Printer.WriteTitle(arge.Message);
                WriteLine("Cerrando");
            }
            finally
            {
                Printer.WriteTitle("Ha entrado en FINALLY");
                Printer.Beep(2500, 500, 3);

            }
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("Saliendo");
            Printer.Beep(3000, 1000, 3);
            Printer.WriteTitle("Salido");
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {

            Printer.WriteTitle("Cursos disponibles");


            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Curso {curso.Nombre  }, ID  {curso.UniqueId}");
                }
            }
        }
    }
}