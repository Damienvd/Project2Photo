
import {
  Auth
} from "./components";
import { Router } from "react-router-dom";
import { createBrowserHistory } from "history";

import Routes from "Routes";

const history = createBrowserHistory({
  basename: '',
});

function App() {
  return (
    <Router history={history}>
      <Auth>
        <Routes />
      </Auth>
    </Router>
    
  );
}

export default App;
