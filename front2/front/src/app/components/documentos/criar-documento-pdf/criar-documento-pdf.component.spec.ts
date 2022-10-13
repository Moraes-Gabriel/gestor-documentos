import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CriarDocumentoPdfComponent } from './criar-documento-pdf.component';

describe('CriarDocumentoPdfComponent', () => {
  let component: CriarDocumentoPdfComponent;
  let fixture: ComponentFixture<CriarDocumentoPdfComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CriarDocumentoPdfComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CriarDocumentoPdfComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
