Algoritmo JUGUETERIA_DONA_JUANITA
    // Vectores Paralelos
    Dimension nombres[200], stock[200], precios[200], almacenes[200]
    Dimension historialVentas[500]
    
    // Variables globales de control
    totalReg <- 5; totalVentasHistorial <- 0; cajaEfectivo <- 0.0; cajaYape <- 0.0
    
    Mientras opcion <> 6 Hacer
        Limpiar Pantalla
        Escribir "=== MENÚ DE CONTROL DE OPERACIONES ==="
        Escribir "[1] REGISTRAR TRANSACCIÓN DE VENTA"
        Escribir "[2] INGRESO DE LOTE COMERCIAL"
        Escribir "[3] REPORTE GENERAL DE INVENTARIO COMPLETO"
        Escribir "[4] REPORTE CONSOLIDADO FINANCIERO DE CAJA"
        Escribir "[5] VER HISTORIAL DE VENTAS DEL DÍA"
        Escribir "[6] FINALIZAR EJECUCIÓN Y SALIDA"
        Escribir "Seleccione una operación (1-6): " Sin Saltar
        Leer opcion
        
        Segun opcion Hacer
            1:
                Escribir "=== REGISTRAR VENTA ==="
                // Lógica para buscar por nombre, restar stock y sumar a cajaEfectivo o cajaYape
            2:
                Escribir "=== INGRESO DE LOTE ==="
                // Lógica para agregar nuevo producto al vector aumentándole a totalReg
            3:
                Escribir "=== INVENTARIO GENERAL ==="
                // Bucle Para desde 1 hasta totalReg mostrando los datos tabulados
            4:
                Escribir "=== BALANCE DE ARQUEO DE CAJA ==="
                Escribir "Efectivo: S/. ", cajaEfectivo
                Escribir "Yape: S/. ", cajaYape
                Escribir "Total: S/. ", (cajaEfectivo + cajaYape)
            5:
                Escribir "=== HISTORIAL DE TRANSACCIONES ==="
                // Bucle Para mostrando el vector historialVentas
            6:
                Escribir "Saliendo del entorno..."
            De Otro Modo:
                Escribir "Opción inválida"
        FinSegun
        
        Si opcion <> 6 Entonces
            Escribir "Presione una tecla para continuar..."
            Leer esperarTecla
        FinSi
    FinMientras
FinAlgoritmo