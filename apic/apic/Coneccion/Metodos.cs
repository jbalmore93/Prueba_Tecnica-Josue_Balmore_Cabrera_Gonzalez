using apic.Modelo;

namespace apic.Coneccion
{
    public class Metodos
    {
        public static string col = "", pare = "", resultado = "";
        public static int res(string color, string col, int numero, int num, string par, string pare)
        {
            int opcion = 0;
            if (numero != 0 && col != null)
            {
                if (numero == num && col == color)
                {
                    opcion = 1;
                }
            }
            else if (col == color)
            {
                opcion = 2;
            }
            else if (par == pare)
            {
                opcion = 3;
            }
            else
            {
                opcion = 4;
            }
            return opcion;
        }
        public static object obj(Ruleta ruleta)
        {
            int numero = ruleta.Numero;
            string color = ruleta.Color;
            string par = ruleta.Par;
            double apuesta = ruleta.Apuesta;
            double restante = ruleta.Puntos;
            int num = 0;
            num = new Random().Next(1, 36);
            if (num % 2 == 0)
            {
                col = "negro";
                pare = "par";
            }
            else
            {
                col = "rojo";
                pare = "impar";
            }
            int opcion = res(color, col, numero, num, par, pare);
            switch (opcion)
            {
                case 1:
                    resultado = "Ganaste Color y Numero";
                    restante = restante + (apuesta * 3);
                    break;
                case 2:
                    resultado = "Ganaste Color";
                    restante = restante + (apuesta / 2);
                    break;
                case 3:
                    resultado = "Acertaste " + pare;
                    restante = restante + apuesta;
                    break;
                default:
                    resultado = "Perdiste";
                    restante = restante - apuesta;
                    break;
            }

            object ob = new object();
            ob = new
            {
                numero = num,
                colores = col,
                pares = pare,
                resultado = resultado,
                restante = restante,

            };
            return ob;
        }
    }
}
