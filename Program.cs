using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PComentario
{
    class Comentario
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public DateTime Fecha{ get; set; }
        public string ComentarioI { get; set; }
        public string Ip { get; set; }
        public int Es_inapropiado { get; set; }
        public int Likes { get; set; }

        public override string ToString()
        {
            return String.Format($"Id:{Id} Comentario: {ComentarioI} - {Autor} - {Ip}- {Fecha} Likes:{Likes} - Es inapropiado: {Es_inapropiado}");
        }
    }

    class ComentarioDB
    {
        public static void SaveToFile(List<Comentario> comentarios, string path)
        {
            StreamWriter textOut = null;
            try
            {

                textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                foreach (var comentario in comentarios)
                {
                    textOut.Write(comentario.Id + "|");
                    textOut.Write(comentario.Autor + "|");
                    textOut.Write(comentario.Fecha + "|");
                    textOut.Write(comentario.ComentarioI + "|");
                    textOut.Write(comentario.Ip + "|");
                    textOut.Write(comentario.Es_inapropiado + "|");
                    textOut.WriteLine(comentario.Likes);
                }
            }

            catch (IOException)
            {
                Console.WriteLine("Ya existe el archivo");
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }

            finally
            {
                if (textOut != null)
                    textOut.Close();
            }

        }

        public static List<Comentario> ReadFromFile(string path)
        {
            List<Comentario> comentarios = new List<Comentario>();
            StreamReader textIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
            try
            {
                while (textIn.Peek() != -1) //Leer hasta el final
                {
                    string row = textIn.ReadLine();
                    string[] columns = row.Split('|');
                    Comentario c = new Comentario();
                    c.Id = int.Parse(columns[0]);
                    c.Autor = columns[1];
                    c.Fecha = DateTime.Parse(columns[2]);
                    c.ComentarioI = columns[3];
                    c.Ip = columns[4];
                    c.Es_inapropiado = int.Parse(columns[5]);
                    c.Likes = int.Parse(columns[6]);
                    comentarios.Add(c);
                }
            }

            catch(IOException)
            {
                Console.WriteLine("El tipo de archivo es incorrecto");
            }

            catch (Exception)
            {
                Console.WriteLine("Error");
            }

            finally
            {
                textIn.Close();
            }
            return comentarios;
        }


        /*
        public static void GetId(int Id, string path)
        {
            List<Comentario> comentarios;
                comentarios = ReadFromFile(path);

            var filtro_id = from c in comentarios
                            where c.Id == Id
                                   select c;

            foreach (var c in filtro_id)
                Console.WriteLine(c);
        }
        */

        public static void GetLike(string path)
        {
            List<Comentario> comentarios;
                comentarios = ReadFromFile(path);
         
            var orden_likes = from c in comentarios
                                   orderby c.Likes descending
                                   select c;

            foreach (var c in orden_likes)
                Console.WriteLine(c);
        }

        public static void GetFecha(string path)
        {
            List<Comentario> comentarios;
            comentarios = ReadFromFile(path);

            var orden_fecha = from c in comentarios
                              orderby c.Fecha descending
                              select c;

            foreach (var c in orden_fecha)
                Console.WriteLine(c);
        }
        
    }


    class Program
    {
        static void Main(string[] args)
        {
            /*
            //Creación de comentarios

            List<Comentario> comentarios = new List<Comentario>();


            comentarios.Add(new Comentario() { Id = 125, Autor = "Pedro", Fecha = new DateTime(2016, 5, 28), ComentarioI = "Buen video", Ip = "193.168.5", Es_inapropiado = 101, Likes = 1000});
            comentarios.Add(new Comentario() { Id = 126, Autor = "Alan", Fecha = new DateTime(2021, 4, 15), ComentarioI = "Genial video", Ip = "193.168.6", Es_inapropiado = 95, Likes = 530 });
            comentarios.Add(new Comentario() { Id = 127, Autor = "Sergio", Fecha = new DateTime(2013, 6, 6), ComentarioI = "Gran video", Ip = "193.168.7", Es_inapropiado = 200, Likes = 540 });
            comentarios.Add(new Comentario() { Id = 128, Autor = "Mario", Fecha = new DateTime(2010, 4, 5), ComentarioI = "Mal video", Ip = "193.168.8", Es_inapropiado = 0, Likes = 150 });
            comentarios.Add(new Comentario() { Id = 129, Autor = "Juan", Fecha = new DateTime(2003, 6, 5), ComentarioI = "Pésimo video", Ip = "193.168.9", Es_inapropiado = 55, Likes = 50 });
            

            ComentarioDB.SaveToFile(comentarios, @"C:\Users\titan\comentarios.txt");
            ComentarioDB.ReadFromFile(@"C:\Users\titan\comentarios.txt");
            */


            //Impresión de comentarios
            Console.WriteLine("Comentarios normales");
            List<Comentario> comentarios = ComentarioDB.ReadFromFile(@"C:\Users\titan\comentarios.txt");
            foreach (var c in comentarios)
                Console.WriteLine(c);


            // Ordenar por fecha y likes
            Console.WriteLine("Comentarios por likes");
            ComentarioDB.GetLike(@"C:\Users\titan\comentarios.txt");
            Console.WriteLine("Comentarios por fecha");
            ComentarioDB.GetFecha(@"C:\Users\titan\comentarios.txt");


            //C# 3.0    LINQ filtro inapropiados
            Console.WriteLine("Comentarios apropiados");
            var filtro_inapropiados = from c in comentarios
                               where c.Es_inapropiado <= c.Likes
                               select c;
            foreach (var c in filtro_inapropiados)
                Console.WriteLine(c);


            // Agregar comentario 
            Console.WriteLine("¿Desea agregar un comentario?");
            Console.WriteLine("Escribe si o no");

            try
            {
                string des = Console.ReadLine();
                if (des == "si")
                {
                    Console.WriteLine("Escribe tu Id");
                    int z = int.Parse(Console.ReadLine());
                    Console.WriteLine("Autor");
                    string x = Console.ReadLine();
                    Console.WriteLine("Fecha obtenida");
                    DateTime c = DateTime.Today;
                    Console.WriteLine("Escribe tu comentario");
                    string v = Console.ReadLine();
                    Console.WriteLine("IP");
                    string b = Console.ReadLine();

                    int n = 0;
                    int m = 0;
                    comentarios.Add(new Comentario() { Id = z, Autor = x, Fecha = c, ComentarioI = v, Ip = b, Es_inapropiado = n, Likes = m });
                    
                }

            }
            catch(FormatException)
            {
                Console.WriteLine("Error de formato");
            }

            catch (Exception)
            {
                Console.WriteLine("Error");
            }

            ComentarioDB.SaveToFile(comentarios, @"C:\Users\titan\comentarios.txt");

        }
    }
}
