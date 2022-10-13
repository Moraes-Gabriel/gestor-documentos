import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabelaDocumentosComponent } from './tabela-documentos.component';

describe('TabelaDocumentosComponent', () => {
  let component: TabelaDocumentosComponent;
  let fixture: ComponentFixture<TabelaDocumentosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TabelaDocumentosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TabelaDocumentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
