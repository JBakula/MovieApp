import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecomendedMovieModalComponent } from './recomended-movie-modal.component';

describe('RecomendedMovieModalComponent', () => {
  let component: RecomendedMovieModalComponent;
  let fixture: ComponentFixture<RecomendedMovieModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecomendedMovieModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecomendedMovieModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
