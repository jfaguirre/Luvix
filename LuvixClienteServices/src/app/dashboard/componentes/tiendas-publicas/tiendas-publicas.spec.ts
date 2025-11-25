import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TiendasPublicas } from './tiendas-publicas';

describe('TiendasPublicas', () => {
  let component: TiendasPublicas;
  let fixture: ComponentFixture<TiendasPublicas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TiendasPublicas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TiendasPublicas);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
