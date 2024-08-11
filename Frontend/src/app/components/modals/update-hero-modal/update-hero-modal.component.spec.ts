import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateHeroModalComponent } from './update-hero-modal.component';

describe('UpdateHeroModalComponent', () => {
  let component: UpdateHeroModalComponent;
  let fixture: ComponentFixture<UpdateHeroModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UpdateHeroModalComponent]
    });
    fixture = TestBed.createComponent(UpdateHeroModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
