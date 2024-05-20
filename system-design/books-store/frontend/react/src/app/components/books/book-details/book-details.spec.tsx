import { render } from '@testing-library/react';

import BookDetails from './book-details';

describe('BookDetails', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<BookDetails />);
    expect(baseElement).toBeTruthy();
  });
});
