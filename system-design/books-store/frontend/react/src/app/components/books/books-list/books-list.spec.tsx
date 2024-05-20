import { render } from '@testing-library/react';

import BooksList from './books-list';

describe('BooksList', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<BooksList />);
    expect(baseElement).toBeTruthy();
  });
});
