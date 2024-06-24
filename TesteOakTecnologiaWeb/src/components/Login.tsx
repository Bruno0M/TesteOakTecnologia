import React, { useState } from 'react';
import { Container, Paper, TextField, Button, Typography, Grid } from '@mui/material';
import { login } from '../services/userService';
import { useNavigate } from 'react-router-dom';

const Login: React.FC = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const navigate = useNavigate();


  const handleLogin = async () => {
    try {
      const response = await login(email, password);
      if (response.data && response.data.token) {
        setError('');
        navigate('/home');
      } else {
        setError('Erro ao realizar o login. Verifique suas credenciais.');
      }
    } catch (error) {
      setError('Erro ao realizar o login. Tente novamente mais tarde.');
      console.error('Erro ao realizar o login:', error);
    }
  };

  return (
    <Container maxWidth="xs">
      <Paper elevation={3} style={{ padding: 20, marginTop: 50 }}>
        <Typography variant="h5" gutterBottom>
          Login
        </Typography>
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <TextField
              label="Email"
              type="email"
              fullWidth
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              label="Senha"
              type="password"
              fullWidth
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </Grid>
          <Grid item xs={12}>
            <Typography variant="body2" color="error">
              {error}
            </Typography>
          </Grid>
          <Grid item xs={12}>
            <Button variant="contained" color="primary" fullWidth onClick={handleLogin}>
              Login
            </Button>
          </Grid>
        </Grid>
      </Paper>
    </Container>
  );
};

export default Login;
