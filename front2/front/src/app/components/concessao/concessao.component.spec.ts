import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConcessaoComponent } from './concessao.component';

describe('ConcessaoComponent', () => {
  let component: ConcessaoComponent;
  let fixture: ComponentFixture<ConcessaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConcessaoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConcessaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
