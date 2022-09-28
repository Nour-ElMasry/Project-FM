import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { useForm } from "react-hook-form";
import GeneralAxiosService from '../../services/GeneralAxiosService';
import { useState } from 'react';
import { Navigate } from 'react-router-dom';

export default function SignIn() {
    const { register, handleSubmit, formState: { errors }} = useForm();
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    const handleCredentialsSubmit = (data) => {
      var credentials = {
        username: data['username'],
        password: data['password'],
      };
      GeneralAxiosService.postMethod("https://localhost:7067/api/v1/Users/Auth", credentials)
      .then((res) => console.log(res.data))
      .then(() => setTimeout(() => {
        setIsLoggedIn(true)
     }, 3000));
    };

    if (isLoggedIn){
        return <Navigate to='/' />
    }

    return <>
        <section className='LoginSection container container--pa'>
        <Container className="FormsPosition container--styled container--pa" maxWidth="xs">
          <Box
            sx={{
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
            }}
          >
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
              <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
              Log in
            </Typography>
            <Box component="form" onSubmit={handleSubmit(handleCredentialsSubmit)}>
              <TextField
                margin="normal"
                required
                fullWidth
                id="username"
                error={!!errors['username']}
                helperText={errors['username']?.message}
                {...register('username', { required: true })}
                label="Username"
                name="username"
                autoComplete='off'
                autoFocus
              />
              <TextField
                margin="normal"
                required
                fullWidth
                name="password"
                label="Password"
                type="password"
                id="password"
                error={!!errors['password']}
                helperText={errors['password']?.message}
                {...register('password', { required: true })}
                autoComplete="current-password"
              />
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
              >
                Sign In
              </Button>
              <Grid container>
                <Grid item>
                  <Link href="#" variant="body2">
                    {"Don't have an account? Sign Up"}
                  </Link>
                </Grid>
              </Grid>
            </Box>
          </Box>
        </Container>
        </section>
    </>;
}