import { CrearUsuarioAPI } from '../../../interface/usuarios.models';
import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, FormBuilder } from '@angular/forms';
import { UsuarioService } from '../../../services/usuario-services/usuario-service';
import { Router } from '@angular/router';
import { apellidoValidator, emailValidator, nombreValidator, passwordlValidator, passwordMatchValidator, maxLengthCustom, minLengthCustom } from '../../../validaciones/validaciones';

@Component({
  selector: 'app-form-registro',
  imports: [ReactiveFormsModule],
  templateUrl: './form-registro.html',
  styleUrl: './form-registro.css',
  standalone: true,
})

export class FormRegistro {

  private readonly formBuilder = inject(FormBuilder);
  usuarioService = inject(UsuarioService);
  router = inject(Router);

  userForm: FormGroup = new FormGroup({

      nombre: new FormControl("", [
        Validators.required,
        minLengthCustom(3),
        maxLengthCustom(50),
        nombreValidator()]),

      apellido: new FormControl("", [
        Validators.required,
        minLengthCustom(3),
        maxLengthCustom(50),
        apellidoValidator()]),

      genero: new FormControl("", [
        Validators.required]),

      email: new FormControl("", [
        Validators.required,
        Validators.maxLength(255),
        Validators.minLength(3),
        Validators.email,
        emailValidator()]),

      password: new FormControl("", [
        Validators.required,
        Validators.maxLength(255),
        Validators.minLength(8),
        passwordlValidator()]),

      password_confirmacion: new FormControl("", [
        Validators.required])
  }, { validators: passwordMatchValidator()});


  guardarCambio()
  {
    const formValue = this.userForm.value;

    const usuario: CrearUsuarioAPI = {
      nombre: formValue.nombre,
      apellido: formValue.apellido,
      genero: formValue.genero,
      email: formValue.email,
      password: formValue.password
      };

    this.usuarioService.crear(usuario).subscribe({
      next: () => {
        this.router.navigate(['/sesion']);
      },
      error: (err) => {
        console.error('Error al crear usuario:', err);
      }
    });
  }
}
