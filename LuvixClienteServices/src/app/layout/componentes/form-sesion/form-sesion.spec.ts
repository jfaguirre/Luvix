import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormSesion } from './form-sesion';

describe('FormSesion', () => {
  let component: FormSesion;
  let fixture: ComponentFixture<FormSesion>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormSesion]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormSesion);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
