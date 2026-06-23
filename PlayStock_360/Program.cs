using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStock_360
{
    internal class Program
    {
        // Estructuras de almacenamiento global (Vectores Paralelos para los 50 productos iniciales)
        static string[] nombres = new string[200];
        static int[] stock = new int[200];
        static double[] precios = new double[200];
        static string[] pagos = new string[200];
        static int[] almacenes = new int[200];

        static int totalReg = 0;

        // Estructura para el Historial Cronológico de Ventas
        static string[] historialVentas = new string[500];
        static int totalVentasHistorial = 0;

        // Variables de Control Financiero (La caja inicia estrictamente en S/. 0.00)
        static double cajaEfectivo = 0.0;
        static double cajaYape = 0.0;

        static void Main(string[] args)
        {
            // Cargar el inventario maestro con los 50 juguetes y sus precios base
            CargarInventarioReal();
            int opcion = 0;

            while (opcion != 6)
            {
                RenderizarInterfazHolografica();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(" || >> SELECCIONE UNA OPERACIÓN (1-6): ");
                Console.ForegroundColor = ConsoleColor.White;

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    MostrarMensajeError("ERROR: ENTRADA SINTÁCTICA INVÁLIDA. INGRESE UN NÚMERO.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        RegistrarVentaReal();
                        break;
                    case 2:
                        IngresarLote();
                        break;
                    case 3:
                        MostrarTodosLosProductos();
                        break;
                    case 4:
                        ReporteCajaReal();
                        break;
                    case 5:
                        MostrarHistorialVentas();
                        break;
                    case 6:
                        EjecutarSalidaSegura();
                        break;
                    default:
                        MostrarMensajeError("ALERTA: OPCIÓN FUERA DE RANGO OPERATIVO.");
                        break;
                }
            }
        }

        static void RenderizarInterfazHolografica()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("============================================================================");
            Console.WriteLine("    PLAYSTOCK 360 // LOGISTICS SYSTEMS & DIGITAL ARCHITECTURE               ");
            Console.WriteLine("============================================================================");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("============================================================================");
            Console.WriteLine("                        MENÚ DE CONTROL DE OPERACIONES                      ");
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine("  [1] REGISTRAR TRANSACCIÓN DE VENTA (DESCONTAR STOCK Y SUMAR CAJA)      ");
            Console.WriteLine("  [2] INGRESO DE LOTE COMERCIAL (ABASTECIMIENTO)                          ");
            Console.WriteLine("  [3] REPORTE GENERAL DE INVENTARIO COMPLETO (VER TODOS LOS PRODUCTOS)    ");
            Console.WriteLine("  [4] REPORTE CONSOLIDADO FINANCIERO DE CAJA                              ");
            Console.WriteLine("  [5] VER HISTORIAL DE VENTAS DEL DÍA (LOG DE OPERACIONES)                ");
            Console.WriteLine("  [6] FINALIZAR EJECUCIÓN Y SALIDA DEL ENTORNO                            ");
            Console.WriteLine("============================================================================");
        }

        // OPCIÓN 1: Registrar una venta real (Busca el juguete, valida stock, descuenta y suma a la caja)
        static void RegistrarVentaReal()
        {
            Console.Clear();
            ImprimirEncabezadoOpcion("REGISTRAR TRANSACCIÓN DE VENTA");

            Console.Write("-> Ingrese el nombre del juguete a vender: ");
            string buscarNom = Console.ReadLine().ToUpper();
            bool encontrado = false;

            for (int i = 0; i < totalReg; i++)
            {
                if (nombres[i] != null && nombres[i].Equals(buscarNom, StringComparison.OrdinalIgnoreCase))
                {
                    encontrado = true;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\n[PRODUCTO DETECTADO] Almacén: [0{almacenes[i]}] | Stock Actual: {stock[i]} unds | Precio: S/. {precios[i]:F2}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("-> Cantidad a vender: ");
                    int cantAVender;
                    while (!LeerEntero("-> Cantidad a vender: ", out cantAVender)) { }

                    if (cantAVender > stock[i])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n[ERROR] Stock insuficiente. Solo quedan {stock[i]} unidades disponibles.");
                        PausarConsola();
                        return;

                    }

                    Console.Write("-> Método de pago (Efectivo/Yape): ");
                    string metodoPago = Console.ReadLine();

                    if (metodoPago.Equals("yape", StringComparison.OrdinalIgnoreCase)) metodoPago = "Yape";
                    if (metodoPago.Equals("efectivo", StringComparison.OrdinalIgnoreCase)) metodoPago = "Efectivo";

                    // Operación Algorítmica: Descontar existencias del vector
                    stock[i] -= cantAVender;

                    // Operación Financiera: Acumular en la caja real correspondiente
                    double montoTotal = cantAVender * precios[i];
                    if (metodoPago.Equals("Efectivo", StringComparison.OrdinalIgnoreCase))
                    {
                        cajaEfectivo += montoTotal;
                    }
                    else
                    {
                        cajaYape += montoTotal;
                    }

                    // Registrar en el Log de Auditoría (Historial)
                    string registroHistorial = $" Ticket N° {totalVentasHistorial + 1:D3} | {nombres[i],-25} | Cant: {cantAVender,3} unds. | Total: S/. {montoTotal,7:F2} | Pago: {metodoPago}";
                    historialVentas[totalVentasHistorial] = registroHistorial;
                    totalVentasHistorial++;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n[SUCCESS] Venta procesada. Total cobrado: S/. {montoTotal:F2}");
                    Console.WriteLine($"[INFO] Nuevo stock de {nombres[i]}: {stock[i]} unidades.");
                    PausarConsola();
                    return;
                }
            }

            if (!encontrado)
            {
                MostrarMensajeError("El juguete no existe en el catálogo maestro.");
            }
        }

        // OPCIÓN 2: Ingresar nuevo lote comercial (Abastecimiento de almacenes)
        static void IngresarLote()
        {
            Console.Clear();
            ImprimirEncabezadoOpcion("INGRESO DE LOTES (ABASTECIMIENTO)");
            Console.Write("-> Nombre del nuevo artículo/lote: ");
            string nuevoNom = Console.ReadLine().ToUpper();
            int cant;
            while (!LeerEntero("-> Cantidad que ingresa: ", out cant)) { }

            double precio;
            while (!LeerDecimal("-> Precio de venta unitario: ", out precio)) { }

            int alm;
            while (!LeerEntero("-> Asignar Almacén destino (1-3): ", out alm)) { }

            nombres[totalReg] = nuevoNom;
            stock[totalReg] = cant;
            precios[totalReg] = precio;
            pagos[totalReg] = "Abastecimiento";
            almacenes[totalReg] = alm;
            totalReg++;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[SUCCESS] Lote registrado e indexado al sistema de almacenes.");
            PausarConsola();
        }

        // OPCIÓN 3: Reporte General de Inventarios (Muestra de forma tabular todos los productos)
        static void MostrarTodosLosProductos()
        {
            Console.Clear();
            ImprimirEncabezadoOpcion("INVENTARIO GENERAL DE PRODUCTOS REGISTRADOS");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {"N°",-4} | {"DESCRIPCIÓN DEL JUGUETE",-30} | {"PRECIO",-10} | {"STOCK",-8} | {"UBICACIÓN",-10}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < totalReg; i++)
            {
                if (nombres[i] != null)
                {
                    string AlertaRojo = stock[i] < 5 ? "[!] " : "    ";
                    Console.WriteLine($" {(i + 1):D3}  | {nombres[i],-30} | S/. {precios[i],6:F2} | {stock[i],4} unds {AlertaRojo} | Almacén 0{almacenes[i]}");
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" [*] Total de ítems únicos mapeados en memoria: {totalReg} productos.");
            PausarConsola();
        }

        // OPCIÓN 4: Reporte Financiero de Caja Real
        static void ReporteCajaReal()
        {
            Console.Clear();
            ImprimirEncabezadoOpcion("BALANCE DE ARQUEO DE CAJA");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" [+] Arqueo Efectivo Físico : S/. {cajaEfectivo:F2}");
            Console.WriteLine($" [+] Conciliación Yape (QR)   : S/. {cajaYape:F2}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" ============================================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" [*] BALANCE MONETARIO TOTAL : S/. {(cajaEfectivo + cajaYape):F2}");
            PausarConsola();
        }

        // OPCIÓN 5: Mostrar Historial de Tickets Emitidos
        static void MostrarHistorialVentas()
        {
            Console.Clear();
            ImprimirEncabezadoOpcion("HISTORIAL DE TRANSACCIONES DEL DÍA");

            if (totalVentasHistorial == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" [!] No se registran transacciones comerciales en el ciclo de ejecución actual.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("----------------------------------------------------------------------------");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" Listado cronológico de operaciones:");
                Console.WriteLine("----------------------------------------------------------------------------");
                for (int i = 0; i < totalVentasHistorial; i++)
                {
                    Console.WriteLine(historialVentas[i]);
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("----------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" [*] Volumen total: {totalVentasHistorial} transacciones.");
            }
            PausarConsola();
        }

        // OPCIÓN 6: Salida Limpia del Entorno
        static void EjecutarSalidaSegura()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("============================================================================");
            Console.WriteLine("                 PLAYSTOCK 360 // TERMINACIÓN DEL SISTEMA                   ");
            Console.WriteLine("============================================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n === ¡PROCESO FINALIZADO COMPLETAMENTE! CIERRE SEGURO DEL ENTORNO ===\n");
            Console.ResetColor();
        }

        static void ImprimirEncabezadoOpcion(string titulo)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"============================================================================");
            Console.WriteLine($"                    PLAYSTOCK 360 // {titulo}                     ");
            Console.WriteLine($"============================================================================\n");
        }

        static void MostrarMensajeError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[¡!] {error}");
            PausarConsola();
        }
        static bool LeerEntero(string mensaje, out int resultado)
        {
            Console.Write(mensaje);
            bool ok = int.TryParse(Console.ReadLine(), out resultado);
            if (!ok)
                MostrarMensajeError("ERROR: Debe ingresar un número entero válido.");
            return ok;
        }

        static bool LeerDecimal(string mensaje, out double resultado)
        {
            Console.Write(mensaje);
            bool ok = double.TryParse(Console.ReadLine(), out resultado);
            if (!ok)
                MostrarMensajeError("ERROR: Debe ingresar un valor numérico válido.");
            return ok;
        }

        static void PausarConsola()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\nPresione cualquier tecla para retornar al panel maestro...");
            Console.ResetColor();
            Console.ReadKey();
        }

        static void CargarInventarioReal()
        {
            string[] items = new string[50] {
                "MUÑECA CON COCHE", "CARRITO A CONTROL", "LEGO DE 577ps", "LEGO DE 400PS", "AJEDRES IMANTADO",
                "SQUISH DE STICH", "SQUISH DE GUERRERAS K-POP", "MUÑECA CON FAMILIA", "COLECCIÓN DRAGON BALL X5", "COLECCIÓN DRAGON BALL X3",
                "COLECCIÓN GRANJA ZENON", "COLECCIÓN VENGADORES", "COLECCIÓN PEPA PIG", "OLLITAS DE METAL", "SONAJA X3",
                "SONAJA  X7", "AVION CON LUCES", "CARRITOS LLANTONES", "COCHE REFORSADO", "TRAILE TRANSPORTADOR",
                "TRAILE TRUCK", "CARRITO SEMI-DEPORTIVO", "PISTOLA HIDROGEL", "PISTOLA DE DARDOS", "PISTOLA CON LUCES Y SONIDO",
                "METRALLETA CON LAZER", "BEBES MELLIZOS", "MUÑECO CON CONEJO", "MUÑECO CON BIBERON", "CARRITOS DE COLECCIÓN",
                "MOTO DEPORTIVA COLECIONABLE", "CELULAR CON MUSICA Y LUZ", "MICROFONO KARAOKE", "SET DE TE ARIEL", "SET DE TE PORCELANA",
                "PIANO MUSICAL", "ORGANO GRANDE", "GUITARRA CON LUZ Y SONIDO", "TAMBOR PEQUEÑO", "XILOFONO",
                "MONOPOLY PERÚ", "LUDO MAGNÉTICO", "JUEGA JUEGA INFANTIL", "BLOQUES BALDE X100", "ROMPECABEZAS 3D",
                "SET DE DOCTORA", "SET DE CONSTRUTOR", "COCINITA CON ACCESORIOS", "MÁQUINA DE BURBUJAS", "PELOTA DE FUTBOL N5"
            };

            double[] preciosExcel = new double[50] {
                43.0, 45.0, 78.0, 55.0, 24.0, 5.0, 10.0, 18.0, 69.0, 39.0,
                149.0, 155.0, 125.0, 39.0, 18.0, 29.0, 33.0, 35.0, 75.0, 43.0,
                53.0, 24.0, 53.0, 10.0, 29.0, 29.0, 99.0, 49.0, 49.0, 33.0,
                27.0, 5.0, 63.0, 49.0, 39.0, 43.0, 79.0, 39.0, 18.0, 24.0,
                59.0, 22.0, 15.0, 45.0, 28.0, 35.0, 39.0, 85.0, 32.0, 25.0
            };

            for (int i = 0; i < items.Length; i++)
            {
                nombres[i] = items[i];
                precios[i] = preciosExcel[i];
                stock[i] = (i % 6 == 0) ? 3 : 15;
                pagos[i] = "Registro Almacén";
                almacenes[i] = (i % 3) + 1;
                totalReg++;
            }
        }
    }
}