import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalheDocumentoComponent } from './detalhe-documento.component';

describe('DetalheDocumentoComponent', () => {
  let component: DetalheDocumentoComponent;
  let fixture: ComponentFixture<DetalheDocumentoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetalheDocumentoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalheDocumentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
