import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

// Validador personalizado
export const passwordMatchValidator: ValidatorFn = (formGroup: AbstractControl): ValidationErrors | null => {
  const password = formGroup.get('password')?.value;
  const confirmPassword = formGroup.get('password_confirmacion')?.value;

  if (password && confirmPassword && password !== confirmPassword) {
    return { passwordMismatch: true };
  }
  return null;
};


import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-form-registro',
  imports: [ReactiveFormsModule],
  templateUrl: './form-registro.html',
  styleUrl: './form-registro.css',
  standalone: true,
})


export class FormRegistro {

  userForm: FormGroup = new FormGroup({

      nombre: new FormControl("", [
        Validators.required,
        Validators.maxLength(50),
        Validators.minLength(3)]),

      apellido: new FormControl("", [
        Validators.required,
        Validators.maxLength(50),
        Validators.minLength(3)]),

      genero: new FormControl("", [
        Validators.required]),

      // departamento: new FormControl("", [
      //   Validators.required]),

      // municipio: new FormControl("", [
      //   Validators.required]),

      email: new FormControl("", [
        Validators.required,
        Validators.maxLength(25),
        Validators.minLength(3),
        Validators.email]),

      password: new FormControl("", [
        Validators.required,
        Validators.maxLength(25),
        Validators.minLength(8)]),

      password_confirmacion: new FormControl("", [
        Validators.required, ])},
        { validators: passwordMatchValidator});
}
