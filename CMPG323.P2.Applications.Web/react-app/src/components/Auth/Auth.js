import React, {
  useEffect,
  useState
} from 'react';
import { useDispatch } from 'react-redux';
import PropTypes from 'prop-types';
import { setUserData, logout } from 'actions/accountActions';
import authService from 'services/authService';
import { LinearProgress } from '@material-ui/core';

const Auth = props => {
  const { children } = props;
  const dispatch = useDispatch();
  const [isLoading, setLoading] = useState(true);

  useEffect(() => {
    const initAuth = async () => {
      try {
        authService.setAxiosInterceptors({
          onLogout: () => dispatch(logout())
        });
  
        authService.handleAuthentication();
  
        if (authService.isAuthenticated()) {
          const user = await authService.loginInWithToken();
  
          await dispatch(setUserData(user));
        }
      } catch {
      } finally {
        setLoading(false);
      }
    };

    initAuth();
  }, [dispatch]);

  if (isLoading) {
    return <LinearProgress />;
  }

  return children;
}

Auth.propTypes = {
  children: PropTypes.any
};

export default Auth;
