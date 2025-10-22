import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FotoPerfil } from './foto-perfil';

describe('FotoPerfil', () => {
  let component: FotoPerfil;
  let fixture: ComponentFixture<FotoPerfil>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FotoPerfil]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FotoPerfil);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
