import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndiceDocumentosTabelaComponent } from './indice-documentos-tabela.component';

describe('IndiceDocumentosTabelaComponent', () => {
  let component: IndiceDocumentosTabelaComponent;
  let fixture: ComponentFixture<IndiceDocumentosTabelaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndiceDocumentosTabelaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IndiceDocumentosTabelaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
