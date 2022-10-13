import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CriarConcessaoComponent } from './criar-concessao.component';

describe('CriarConcessaoComponent', () => {
  let component: CriarConcessaoComponent;
  let fixture: ComponentFixture<CriarConcessaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CriarConcessaoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CriarConcessaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
