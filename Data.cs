
using System.Net;
using System.Net.Sockets;

//  865284041062674

namespace Data
{
    public class DataTrabajos
    {
        /*
         * Nro_Orden int
         * Apellido_Cliente varchar(30)
         * Nombre_Cliente varchar(30)
         * DNI varchar(12) 
         * Telefono varchar(20)
         * Domicilio varchar(50)
         * Tipo_Cristal varchar(20)
         * AOIEL real
         * AOICL real
         * Eje_OIL real
         * AODEL real
         * AODCL real
         * Eje_ODL real
         * AOIEC real
         * AOICC real
         * Eje_OIC real
         * AODEC real
         * AODCC real
         * Eje_ODC real
         * Fecha_Pedido varchar(20)
         * Fecha_Entrega_Pactada varchar(20)
         * Fecha_Entrega_Real varchar(20)
         */

        public static bool anteojo_cerca { get; set; }
        public static bool anteojo_lejos { get; set; }
        public static int Nro_Orden { get; set; }
        public static int Nro_GiftCard { get; set; }
        public static string Fecha_Nac_Cliente { get; set; }
        public static string Apellido_Cliente { get; set; }
        public static string Nombre_Cliente { get; set; }
        public static string DNI_Cliente { get; set; }
        public static string Email_Cliente { get; set; }
        public static string Telefono_Cliente { get; set; }
        public static bool WhatsApp { get; set; }
        public static bool Celular { get; set; }
        public static string Domicilio_Cliente { get; set; }
        public static string Tipo_Cristal_Cerca { get; set; }
        public static string Tipo_Cristal_Lejos { get; set; }
        public static double AOIEL { get; set; }
        public static double AOICL { get; set; }
        public static double Eje_OIL { get; set; }
        public static double AODEL { get; set; }
        public static double AODCL { get; set; }
        public static double Eje_ODL { get; set; }
        public static double AOIEC { get; set; }
        public static double AOICC { get; set; }
        public static double Eje_OIC { get; set; }
        public static double AODEC { get; set; }
        public static double AODCC { get; set; }
        public static double Eje_ODC { get; set; }
        public static double Inv_AOEIL { get; set; }
        public static double Inv_AOICL { get; set; }
        public static double Inv_Eje_OIL { get; set; }
        public static double Inv_AODEL { get; set; }
        public static double Inv_AODCL { get; set; }
        public static double Inv_Eje_ODL { get; set; }
        public static double Inv_AOIEC { get; set; }
        public static double Inv_AOICC { get; set; }
        public static double Inv_Eje_OIC { get; set; }
        public static double Inv_AODEC { get; set; }
        public static double Inv_AODCC { get; set; }
        public static double Inv_Eje_ODC { get; set; }
        public static string Fecha_Pedido { get; set; }
        public static string Fecha_Entrega_Pactada { get; set; }
        public static string Fecha_Pedido_Finalizado { get; set; }
        public static string Fecha_Entrega_Real { get; set; }
        public static string Detalle_Reparacion { get; set; }
        public static string PROCEDENCIA { get; set; }
        public static string Armazon_Lejos { get; set; }
        public static string Armazon_Cerca { get; set; }
        public static int Subtotal { get; set; }
        public static int Seña { get; set; }
        public static int Resta_Abonar { get; set; }
        public static bool Subido_A_Firebase { get; set; }
    }

    public class Data_Optica
    {
        public static string Nombre { get; set; }
        public static string WhatsApp { get; set; }
        public static string Domicilio { get; set; }
        public static string Horario { get; set; }
        public static string Instagram { get; set; }
        public static string Facebook { get; set; }
        public static string Email { get; set; }
        public static string Website { get; set; }
        public static int Computadora_Nro { get; set; }
    }


    public class LC_Cosmetica
    {
        public string Marca { get; set; }
        public string Color { get; set; }
        public int Precio { get; set; }
        public int Cantidad_Disponible { get; set; }
    }

    public class Valores_Calculo
    {
        public static int total { get; set; }
        public static int seña { get; set; }
        public static int subtotal { get; set; }
        public static int armazon_lejos { get; set; }
        public static int armazon_cerca { get; set; }
        public static int cristal_lejos { get; set; }
        public static int cristal_cerca { get; set; }
    }

    public class InfoTrabajos
    {
        /*
         * Nro_Orden int
         * Apellido_Cliente varchar(30)
         * Nombre_Cliente varchar(30)
         * DNI varchar(12) 
         * Telefono varchar(20)
         * Domicilio varchar(50)
         * Tipo_Cristal varchar(20)
         * AOIEL real
         * AOICL real
         * Eje_OIL real
         * AODEL real
         * AODCL real
         * Eje_ODL real
         * AOIEC real
         * AOICC real
         * Eje_OIC real
         * AODEC real
         * AODCC real
         * Eje_ODC real
         * Fecha_Pedido varchar(20)
         * Fecha_Entrega_Pactada varchar(20)
         * Fecha_Entrega_Real varchar(20)
         */

        public int Valor_Cristal { get; set; }
        public int Nro_Orden { get; set; }
        public string Apellido_Cliente { get; set; }
        public string Nombre_Cliente { get; set; }
        public string DNI_Cliente { get; set; }
        public string Telefono_Cliente_Prefijo { get; set; }
        public string Telefono_Cliente_Resto { get; set; }
        public string Telefono_Cliente { get; set; }
        public bool WhatsApp { get; set; }
        public string Domicilio_Cliente { get; set; }
        public string Tipo_Cristal { get; set; }
        public double AOEIL { get; set; }
        public double AOICL { get; set; }
        public double Eje_OIL { get; set; }
        public double AODEL { get; set; }
        public double AODCL { get; set; }
        public double Eje_ODL { get; set; }
        public double AOIEC { get; set; }
        public double AOICC { get; set; }
        public double Eje_OIC { get; set; }
        public double AODEC { get; set; }
        public double AODCC { get; set; }
        public double Eje_ODC { get; set; }
        public double Inv_AOEIL { get; set; }
        public double Inv_AOICL { get; set; }
        public double Inv_Eje_OIL { get; set; }
        public double Inv_AODEL { get; set; }
        public double Inv_AODCL { get; set; }
        public double Inv_Eje_ODL { get; set; }
        public double Inv_AOIEC { get; set; }
        public double Inv_AOICC { get; set; }
        public double Inv_Eje_OIC { get; set; }
        public double Inv_AODEC { get; set; }
        public double Inv_AODCC { get; set; }
        public double Inv_Eje_ODC { get; set; }
        public string Fecha_Pedido { get; set; }
        public string Fecha_Entrega_Pactada { get; set; }
        public string Fecha_Pedido_Finalizado { get; set; }
        public string Fecha_Entrega_Real { get; set; }
    }

    class Variables_Globales
    {
        public static string IP_Local { get; set; }
        public static string IP_Broadcast { get; set; }
        public static byte[] incoming_message { get; set; }

        public static bool messageReceived = false;

        public static bool unica_vez = false;

        public static string tipo_cristal_lejos;

        public static string tipo_cristal_cerca;

        public static string armazon_lejos;

        public static string armazon_cerca;

        public static int nro_orden;
        public static int nro_giftcard;
        public static bool celular = false;
        public static bool whatsApp = false;

        public static double AOIEL = 0;
        public static double AOICL = 0;
        public static double Eje_OIL = 0;

        public static double AODEL = 0;
        public static double AODCL = 0;
        public static double Eje_ODL = 0;

        public static double AOIEC = 0;
        public static double AOICC = 0;
        public static double Eje_OIC = 0;

        public static double AODEC = 0;
        public static double AODCC = 0;
        public static double Eje_ODC = 0;

        public static double Inv_AOIEL = 0;
        public static double Inv_AOICL = 0;
        public static double Inv_Eje_OIL = 0;

        public static double Inv_AODEL = 0;
        public static double Inv_AODCL = 0;
        public static double Inv_Eje_ODL = 0;

        public static double Inv_AOIEC = 0;
        public static double Inv_AOICC = 0;
        public static double Inv_Eje_OIC = 0;

        public static double Inv_AODEC = 0;
        public static double Inv_AODCC = 0;
        public static double Inv_Eje_ODC = 0;

        public static int rowIndex = 3;
    }

    public class Firebase_AR
    {
        public static string Cant { get; set; }
        public static string Cod { get; set; }
        public static string Material { get; set; }
        public static string Precio { get; set; }
        public static string Mod { get; set; }
        public static string Marca { get; set; }
    }

    public class Firebase_Liq
    {
        public static string Cant { get; set; }
        public static string Cod { get; set; }
        public static string Precio { get; set; }
        public static string Uni_Tam { get; set; }
        public static string Tamaño { get; set; }
        public static string Marca { get; set; }
    }

    public class Firebase_Cristales
    {
        public static string Cant { get; set; }
        public static string Cod { get; set; }
    }

    public class Firebase_VENTAS
    {
        public static int Amount { get; set; }
        public static int startDate { get; set; }
        public static int endDate { get; set; }
    }
}
