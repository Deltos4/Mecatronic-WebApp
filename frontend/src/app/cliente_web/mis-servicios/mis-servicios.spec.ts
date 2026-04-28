import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MisServicios } from './mis-servicios';

describe('MisServicios', () => {
  let component: MisServicios;
  let fixture: ComponentFixture<MisServicios>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MisServicios]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MisServicios);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
