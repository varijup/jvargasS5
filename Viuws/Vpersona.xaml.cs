

using jvargasS5.Modelos;
using System;
using System.Collections.Generic;


namespace jvargasS5.Viuws;

public partial class Vpersona : ContentPage
{
    public Vpersona()
    {
        InitializeComponent();
    }

    private void btnAgregar_Cliked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        App.personRepo.AddNewPerson(txtPersona.Text);
        lblStatus.Text = App.personRepo.StatusMassage;
        CargarPersonas();
    }


    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        CargarPersonas();
        if (Listapersona.ItemsSource == null || ((List<Persona>)Listapersona.ItemsSource).Count == 0)
        {
            lblStatus.Text = "No hay personas registradas";
        }
    }

    // Método para cargar las personas en la colección
    private void CargarPersonas()
    {
        List<Persona> people = App.personRepo.GetAllPeople();
        Listapersona.ItemsSource = people;

        // Habilitar los botones de eliminar y actualizar solo si hay personas en la lista
        btnEliminar.IsEnabled = people.Count > 0;
        btnModificar.IsEnabled = people.Count > 0;
    }

    // Método para eliminar una persona
    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        if (Listapersona.SelectedItem != null)
        {
            var selectedPerson = (Persona)Listapersona.SelectedItem;
            App.personRepo.DeletePerson(selectedPerson.Id);
            lblStatus.Text = App.personRepo.StatusMassage;
            CargarPersonas();
            btnEliminar.IsEnabled = false; // Desactivar el botón después de eliminar
        }
        else
        {
            lblStatus.Text = "Por favor seleccione una persona para eliminar";
        }
    }
    // Método para actualizar una persona
    private async void btnModificar_Clicked(object sender, EventArgs e)
    {
        if (Listapersona.SelectedItem != null)
        {
            var selectedPerson = (Persona)Listapersona.SelectedItem;
            string newName = await DisplayPromptAsync("Modificar Persona", "Ingrese el nuevo nombre:", "Aceptar", "Cancelar", "", -1, Keyboard.Default, selectedPerson.Name);
            if (!string.IsNullOrEmpty(newName))
            {
                selectedPerson.Name = newName;
                App.personRepo.UpdatePerson(selectedPerson);
                lblStatus.Text = App.personRepo.StatusMassage;
                CargarPersonas();
            }
        }
        else
        {
            lblStatus.Text = "Por favor seleccione una persona para modificar";
        }
    }

    private void Listapersona_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Listapersona.SelectedItem != null)
        {
            btnEliminar.IsEnabled = true;
            btnModificar.IsEnabled = true;
        }
        else
        {
            btnEliminar.IsEnabled = false;
            btnModificar.IsEnabled = false;
        }
    }

}
