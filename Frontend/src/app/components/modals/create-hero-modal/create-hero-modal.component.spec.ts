import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateHeroModalComponent } from './create-hero-modal.component';

describe('CreateHeroModalComponent', () => {
  let component: CreateHeroModalComponent;
  let fixture: ComponentFixture<CreateHeroModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateHeroModalComponent]
    });
    fixture = TestBed.createComponent(CreateHeroModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
