import React, { useState } from 'react';
import { Container, Paper, TextField, Button, Typography, Grid } from '@mui/material';
import { register } from '../services/userService';
import { useNavigate } from 'react-router-dom';

const Register: React.FC = () => {
  const [email, setEmail] = useState('');
  const [name, setName] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');

  const navigate = useNavigate();


  const handleRegister = async () => {
    try {
      const response = await register(name, email, password, confirmPassword);
      if (response.data) {
        navigate('/auth/login');
      } else {
        setError('Erro ao realizar o registro.');
      }
    } catch (error) {
      setError('Erro ao realizar o registro. Tente novamente mais tarde.');
      console.error('Erro ao realizar o registro:', error);
    }
  };

  return (
    <Container maxWidth="xs">
      <Paper elevation={3} style={{ padding: 20, marginTop: 50 }}>
        <Typography variant="h5" gutterBottom>
          Registro
        </Typography>
        <Grid container spacing={2}>
        <Grid item xs={12}>
            <TextField
              label="Nome"
              type="text"
              fullWidth
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
          </Grid>
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
            <TextField
              label="Confirmar Senha"
              type="password"
              fullWidth
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
            />
          </Grid>
          <Grid item xs={12}>
            <Typography variant="body2" color="error">
              {error}
            </Typography>
          </Grid>
          <Grid item xs={12}>
            <Button variant="contained" color="primary" fullWidth onClick={handleRegister}>
              Registrar
            </Button>
          </Grid>
        </Grid>
      </Paper>
    </Container>
  );
};

export default Register;
