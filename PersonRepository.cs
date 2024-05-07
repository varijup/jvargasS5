using jvargasS5.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jvargasS5
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;

        public string StatusMassage { get; set; }

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }
        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }
        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("El nombre es requerido");
                Persona person = new() { Name = name };
                result = conn.Insert(person);


                StatusMassage = string.Format("Dato Agregado", result, name);

            }
            catch (Exception ex)

            {
                StatusMassage = string.Format("Error, NO se inserto", name, ex.Message);
            }


        }
        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();

            }
            catch (Exception ex)
            {

                StatusMassage = string.Format("Error al mostrar los Datos", ex.Message);
            }
            return new List<Persona>();

        }

        // Método para eliminar una persona
        public void DeletePerson(int id)
        {
            try
            {
                Init();
                conn.Delete<Persona>(id);
                StatusMassage = "Persona eliminada correctamente";
            }
            catch (Exception ex)
            {
                StatusMassage = string.Format("Error al eliminar la persona", ex.Message);
            }
        }

        // Método para actualizar una persona
        public void UpdatePerson(Persona person)
        {
            try
            {
                Init();
                conn.Update(person);
                StatusMassage = "Persona actualizada correctamente";
            }
            catch (Exception ex)
            {
                StatusMassage = string.Format("Error al actualizar la persona", ex.Message);
            }
        }

    }
}
