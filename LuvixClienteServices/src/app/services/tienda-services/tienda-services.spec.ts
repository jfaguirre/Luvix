import { TestBed } from '@angular/core/testing';

import { TiendaServices } from './tienda-services';

describe('TiendaServices', () => {
  let service: TiendaServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TiendaServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
