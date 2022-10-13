import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeusDocumentosTabelaComponent } from './meus-documentos-tabela.component';

describe('MeusDocumentosTabelaComponent', () => {
  let component: MeusDocumentosTabelaComponent;
  let fixture: ComponentFixture<MeusDocumentosTabelaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MeusDocumentosTabelaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MeusDocumentosTabelaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
