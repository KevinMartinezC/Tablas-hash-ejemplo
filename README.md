# 🎓 Sistema de Gestión de Estudiantes - Implementación con Dictionary<K,V>

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-0078D6?style=for-the-badge&logo=windows&logoColor=white)

## 📋 Descripción del Proyecto

Este proyecto es una aplicación de **Windows Forms** desarrollada en C# que demuestra el uso práctico de la estructura de datos **Dictionary<TKey, TValue>**, una implementación moderna de las tablas hash en .NET. El sistema permite gestionar información de estudiantes universitarios con operaciones de alta eficiencia en tiempo constante O(1).

## 🎯 Objetivo

Demostrar de manera práctica y visual cómo funcionan las **tablas hash** (específicamente Dictionary en C#), sus ventajas y casos de uso reales en el desarrollo de aplicaciones.

---

## 🏗️ Estructura del Proyecto
```
Tablas_hash_ejemplo/
│
├── Estudiante.cs           # Clase modelo para representar un estudiante
├── Form1.cs                # Lógica principal del formulario
├── Form1.Designer.cs       # Diseño visual del formulario
└── Program.cs              # Punto de entrada de la aplicación
```

---

## 🔑 Conceptos Clave Implementados

### ¿Qué es un Dictionary?

**Dictionary<TKey, TValue>** es la versión moderna y genérica de las tablas hash en C# (.NET). Implementa internamente una tabla hash con las siguientes características:

- ✅ **Almacenamiento de pares clave-valor**
- ✅ **Acceso en tiempo constante O(1)** promedio
- ✅ **Tipado fuerte** (type-safe)
- ✅ **Redimensionamiento automático**
- ✅ **Gestión inteligente de colisiones** mediante encadenamiento
- ✅ **Garantía de unicidad de claves**

### Ventajas sobre otras estructuras:

| Característica | Dictionary | Hashtable | List |
|---------------|-----------|-----------|------|
| Tipado genérico | ✅ | ❌ | ✅ |
| Rendimiento búsqueda | O(1) | O(1) | O(n) |
| Rendimiento inserción | O(1) | O(1) | O(1)* |
| Claves duplicadas | ❌ | ❌ | ✅ |
| Type-safe | ✅ | ❌ | ✅ |
| Versión moderna | ✅ | ❌ (legacy) | ✅ |

*\* O(n) si se inserta al inicio o medio de la lista*

---

## 💻 Características del Sistema

### Operaciones CRUD Completas

1. **➕ Agregar Estudiante**
   - Validación de ID único
   - Verificación de campos obligatorios
   - Operación O(1)

2. **🔍 Buscar Estudiante**
   - Búsqueda por ID en tiempo constante
   - Método `TryGetValue()` para búsqueda segura
   - Resaltado visual en la tabla

3. **✏️ Actualizar Datos**
   - Modificación directa usando `dictionary[key]`
   - Validaciones de integridad
   - Actualización en tiempo real

4. **🗑️ Eliminar Estudiante**
   - Eliminación con confirmación
   - Método `Remove()` en O(1)
   - Actualización automática de la vista

5. **📊 Estadísticas**
   - Uso de LINQ con `Dictionary.Values`
   - Cálculo de promedios (general, máximo, mínimo)
   - Conteo de estudiantes por carrera

6. **🔎 Búsqueda en Tiempo Real**
   - Filtrado dinámico por ID o nombre
   - Actualización instantánea del DataGridView

---

## 🚀 Instalación

### Pasos de Instalación

1. **Clonar o descargar el proyecto**
```bash
   https://github.com/KevinMartinezC/Tablas-hash-ejemplo.git
```

2. **Abrir el proyecto**
   - Abrir Visual Studio
   - File → Open → Project/Solution
   - Seleccionar `Tablas_hash_ejemplo.sln`

## 📖 Guía de Uso

### Interfaz Principal
<img width="1026" height="775" alt="Screenshot 2025-10-24 at 7 26 19 PM" src="https://github.com/user-attachments/assets/51ca6b19-839c-4d94-89cd-3dc5e803fc7d" />

### 1. Agregar un Estudiante

1. Ingresar **ID** único (número entero)
2. Completar **Nombre** del estudiante
3. Ingresar **Carrera**
4. Especificar **Promedio** (0.0 - 10.0)
5. Hacer clic en **➕ Agregar**

### 2. Buscar un Estudiante

1. Ingresar el **ID** en el campo correspondiente
2. Hacer clic en **🔍 Buscar**
3. Los datos se mostrarán en los campos y en la tabla

### 3. Actualizar Información

1. Buscar o seleccionar el estudiante en la tabla
2. Modificar los campos deseados
3. Hacer clic en **✏️ Actualizar**

### 4. Eliminar un Estudiante

1. Ingresar el **ID** o seleccionar de la tabla
2. Hacer clic en **🗑️ Eliminar**
3. Confirmar la eliminación

### 5. Ver Estadísticas

- Hacer clic en **📊 Estadísticas**
- Se mostrará:
  - Total de estudiantes
  - Promedio general
  - Promedio más alto/bajo
  - Distribución por carrera

### 6. Filtrar Resultados

- Usar el campo **"🔍 Buscar (ID/Nombre)"**
- Escribir el ID o nombre para filtrar en tiempo real

---

## 🔬 Explicación Técnica

### Estructura de la Clase Estudiante
```csharp
public class Estudiante
{
    public int ID { get; set; }              // Clave única
    public string Nombre { get; set; }       // Nombre completo
    public string Carrera { get; set; }      // Carrera universitaria
    public double Promedio { get; set; }     // Promedio académico
    public DateTime FechaRegistro { get; set; } // Fecha de alta
}
```

### Declaración del Dictionary
```csharp
// Dictionary con clave int (ID) y valor Estudiante
private Dictionary<int, Estudiante> registroEstudiantes;

// Inicialización
registroEstudiantes = new Dictionary<int, Estudiante>();
```

### Operaciones Principales

#### 1. Agregar (Insert) - O(1)
```csharp
Estudiante nuevo = new Estudiante(id, nombre, carrera, promedio);
registroEstudiantes.Add(id, nuevo);
```

#### 2. Verificar Existencia - O(1)
```csharp
if (registroEstudiantes.ContainsKey(id))
{
    // El ID ya existe
}
```

#### 3. Buscar (Search) - O(1)
```csharp
if (registroEstudiantes.TryGetValue(id, out Estudiante estudiante))
{
    // Estudiante encontrado
    Console.WriteLine(estudiante.Nombre);
}
```

#### 4. Actualizar (Update) - O(1)
```csharp
registroEstudiantes[id].Nombre = nuevoNombre;
registroEstudiantes[id].Promedio = nuevoPromedio;
```

#### 5. Eliminar (Delete) - O(1)
```csharp
registroEstudiantes.Remove(id);
```

#### 6. Iterar sobre elementos
```csharp
foreach (KeyValuePair<int, Estudiante> par in registroEstudiantes)
{
    Console.WriteLine($"ID: {par.Key}, Nombre: {par.Value.Nombre}");
}
```

## 📖 Referencias

### Documentación Oficial

- [Microsoft Docs - Dictionary<TKey,TValue>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)
- [Hash Tables - Wikipedia](https://en.wikipedia.org/wiki/Hash_table)
- [C# Collections](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/collections)

## 📄 Licencia

Este proyecto es de uso educativo y está disponible bajo la licencia MIT.

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Para cambios importantes:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

<div align="center">

**⭐ Si este proyecto te fue útil, considera darle una estrella ⭐**

Made with ❤️ usando C# y Windows Forms

</div>
