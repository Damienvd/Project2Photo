import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { 
  Button, 
  TextField, 
  Grid,
} from '@material-ui/core';

import useRouter from 'utils/useRouter';
import { login } from 'actions/accountActions';
import Page from '../../../components/Page/Page';

const Login = () => {
  const { history } = useRouter();
  const dispatch = useDispatch();
  const [signingIn, setSigningIn] = useState(false);
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleUsernameChange = event => {
    event.persist();
    setUsername(event.target.value);
  }

  const handlePasswordChange = event => {
    event.persist();
    setPassword(event.target.value);
  }

  const handleSubmit = async event => {
    event.preventDefault();

    const loginDetails = {
      username,
      password
    };

    try {
      setSigningIn(true);
      await dispatch(login(loginDetails));
      history.push('/home');
    } catch (error) {
      console.log(error)
      alert("Error")
    } finally {
      setSigningIn(false);
    }
  };

  return (
    <Page title='Login'>
    <form
      onSubmit={handleSubmit}
    >
      <Grid container spacing={2}>
        <Grid
          item
          xs={12}
        >
          <TextField
            fullWidth
            label='Username'
            name='username'
            onChange={handleUsernameChange}
            value={username}
            variant='outlined'
          />
        </Grid>
        <Grid
          item
          xs={12}
        >
          <TextField
            fullWidth
            label='Password'
            name='password'
            onChange={handlePasswordChange}
            value={password}
            variant='outlined'
            type='password'
          />
        </Grid>
        <Grid
          item
          xs={12}
        >
          <Grid container spacing={2} alignItems='center'>
            <Grid 
              item 
              sm={8}
              xs={12}
            >
              <Button
                  fullWidth
                  color='primary'
                  disabled={signingIn}
                  size='medium'
                  type='submit'
                  variant='contained'
                >
                  Sign In
                </Button>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </form>
    </Page>
    
  );
};

export default Login;
