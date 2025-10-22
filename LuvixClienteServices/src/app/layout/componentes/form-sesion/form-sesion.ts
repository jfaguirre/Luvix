import { Token } from '@angular/compiler';
import { response } from 'express';


import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { SesionService } from '../../../services/usuario-services/sesion-service';
import { SesionAPI } from '../../../interface/sesion.models';
import { AuthService } from '../../../services/auth';

@Component({
  selector: 'app-form-sesion',
  imports: [ReactiveFormsModule],
  templateUrl: './form-sesion.html',
  styleUrl: './form-sesion.css',
  standalone: true
})
export class FormSesion {

  private readonly formBuilder = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  sesionService = inject(SesionService);
  router = inject(Router);


  sesionForm: FormGroup = new FormGroup({

    email: new FormControl("", [
        Validators.required,
        Validators.email]),

    password: new FormControl("", [
        Validators.required]),
  })

    sesionUsuario()
    {
      const formValue = this.sesionForm.value;

      const usuario: SesionAPI = {
        email: formValue.email,
        password: formValue.password
        };

      this.sesionService.sesion(usuario).subscribe({
        next: (response) => {

          this.authService.setToken(response.token);
          this.router.navigate(['/dashboard']);
        },
        error: (err) => {
          console.error('Error al iniciar sesión', err);
        }
      });
    }
}
