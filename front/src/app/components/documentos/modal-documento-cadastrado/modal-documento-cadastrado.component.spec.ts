import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalDocumentoCadastradoComponent } from './modal-documento-cadastrado.component';

describe('ModalDocumentoCadastradoComponent', () => {
  let component: ModalDocumentoCadastradoComponent;
  let fixture: ComponentFixture<ModalDocumentoCadastradoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalDocumentoCadastradoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalDocumentoCadastradoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
