import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TiendasSeguidas } from './tiendas-seguidas';

describe('TiendasSeguidas', () => {
  let component: TiendasSeguidas;
  let fixture: ComponentFixture<TiendasSeguidas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TiendasSeguidas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TiendasSeguidas);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
