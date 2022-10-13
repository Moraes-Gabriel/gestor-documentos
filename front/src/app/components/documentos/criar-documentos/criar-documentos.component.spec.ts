import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CriarDocumentosComponent } from './criar-documentos.component';

describe('CriarDocumentosComponent', () => {
  let component: CriarDocumentosComponent;
  let fixture: ComponentFixture<CriarDocumentosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CriarDocumentosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CriarDocumentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
