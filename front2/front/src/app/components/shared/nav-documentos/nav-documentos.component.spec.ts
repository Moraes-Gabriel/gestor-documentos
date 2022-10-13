import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavDocumentosComponent } from './nav-documentos.component';

describe('NavDocumentosComponent', () => {
  let component: NavDocumentosComponent;
  let fixture: ComponentFixture<NavDocumentosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavDocumentosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavDocumentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
