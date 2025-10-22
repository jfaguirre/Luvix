// validators.ts
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

// Nombre
export function nombreValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;

    const regex = /^[a-zA-ZÀ-ÿ]+(?: [a-zA-ZÀ-ÿ]+)*$/;
    const isValid = regex.test(value);

    return isValid ? null : { nombreInvalido: true };
  };
}

// Apellido
export function apellidoValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;

    const regex = /^[a-zA-ZÀ-ÿ]+(?: [a-zA-ZÀ-ÿ]+)*$/;
    const isValid = regex.test(value);

    return isValid ? null : { apellidoInvalido: true };
  };
}


// Email
export function emailValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;

    const regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    const isValid = regex.test(value);

    return isValid ? null : { emailInvalido: true };
  };
}

// Password
export function passwordlValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;

    const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$/;
    const isValid = regex.test(value);

    return isValid ? null : { passwordInvalido: true };
  };
}

// PAssword confirmacion
export function passwordMatchValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('password_confirmacion')?.value;

    if (password && confirmPassword && password !== confirmPassword) {
      return { confirmPasswordValido: true };
    }

    return null;
  };
}

// maxLewngth
export function maxLengthCustom(max: number): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (value && value.toString().length > max) {
      return { maxLengthCustom: { required: max, actual: value.toString().length } };
    }
    return null;
  };
}

// minLength
export function minLengthCustom(min: number): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (value && value.toString().length < min) {
      return { minLengthCustom: { required: min, actual: value.toString().length } };
    }
    return null;
  };
}


