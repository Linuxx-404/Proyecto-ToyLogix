# PlayStock_360

Sistema de consola desarrollado en **C# (.NET)** para automatizar el control de inventario, ventas y caja de la **Juguetería "Doña Juanita"** (Cajamarca, Perú), reemplazando el registro manual en cuadernos físicos por un prototipo informático estructurado.

Proyecto desarrollado para el curso **Fundamentos de Programación (CIIN1205P)** — UPN.

## 📋 Descripción

El negocio gestiona tres almacenes físicos y registra sus ventas de forma manual, lo que genera errores de cálculo y pérdida de información. PlayStock_360 automatiza:

- Registro de ventas (descuento de stock + acumulación en caja)
- Ingreso de nuevos lotes de mercadería
- Reporte general de inventario
- Reporte consolidado de caja (Efectivo / Yape)
- Historial de transacciones del día
- Salida segura del sistema

## 🛠️ Tecnologías y estructuras utilizadas

- **Lenguaje:** C# (.NET Core / .NET Framework, consola)
- **Estructuras de datos:** Arreglos paralelos unidimensionales (string[], int[], double[])
- **Estructuras de control:** while, switch-case, for
- **Validación de entradas:** int.TryParse() / double.TryParse() para evitar caídas del programa ante datos inválidos

## ▶️ Cómo ejecutar el programa

### Requisitos
- Visual Studio 2019 o superior (con la carga de trabajo ".NET desktop development")
- .NET Framework / .NET Core compatible con el proyecto (incluido en la solución)

### Pasos
1. Clona este repositorio:
   git clone https://github.com/Linuxx-404/Proyecto-ToyLogix.git
2. Abre el archivo PlayStock_360.sln con Visual Studio.
3. Presiona Iniciar (F5) o Ctrl+F5 para compilar y ejecutar.
4. Usa el menú numérico (1-6) en la consola para navegar entre las opciones.

> No requiere instalación de paquetes externos ni conexión a internet: toda la información se gestiona en memoria durante la ejecución.

## 📁 Estructura del repositorio

PlayStock_360/
├── PlayStock_360/
│   ├── Program.cs          # Lógica principal del sistema
│   ├── App.config
│   └── Properties/
├── Diagrama/
│   └── JUGUETERIA_DONA_JUANITA.psc   # Diagrama de flujo / pseudocódigo en PSeInt
├── PlayStock_360.slnx
└── README.md

## ⚠️ Limitaciones actuales

- Los datos se almacenan únicamente en memoria RAM (no hay persistencia en archivo o base de datos); al cerrar el programa, la información se pierde.
- No cuenta con interfaz gráfica (GUI); opera completamente en consola.

## 🚀 Mejoras futuras

- Persistencia de datos mediante archivos o una base de datos relacional (SQL Server / SQLite).
- Interfaz gráfica de usuario (GUI) para facilitar el uso por parte del administrador del negocio.

## 👤 Autor

Willian Ander Calderón Polo — Ingeniería de Sistemas y Redes, UPN.
