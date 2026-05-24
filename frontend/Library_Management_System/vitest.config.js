import { defineConfig } from 'vitest/config';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  test: {
    environment: 'jsdom',
    globals: true,
    setupFiles: ['./vitest.setup.js'],
    css: true,
    // Avoid importing the whole app (which imports non-existent relative modules)
    include: ['src/__tests__/**/*.test.*'],
    // Prevent worker startup issues on this Windows setup
    pool: 'vmThreads',
    maxWorkers: 1,

    // Ensure vitest doesn't try to load the browser entry via index.html
    browser: {
      enabled: false
    }
  }
});




