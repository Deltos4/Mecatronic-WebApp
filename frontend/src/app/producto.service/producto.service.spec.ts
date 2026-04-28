import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductoService } from './producto.service';

describe('ProductoService', () => {
  let component: ProductoService;
  let fixture: ComponentFixture<ProductoService>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductoService]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductoService);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
