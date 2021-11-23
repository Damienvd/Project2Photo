import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import { configureStore } from 'store';
import { Provider } from 'react-redux';
import { enableES5 } from 'immer';

enableES5();

const store = configureStore();

ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>
      <App />
    </Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

