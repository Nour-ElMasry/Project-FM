import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { Link, useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import { useState } from 'react';
import { Alert } from '@mui/material';


export default function SignUp() {
  const { register, handleSubmit, formState: { errors }} = useForm();
  const [successfulSignUp, setSuccessfulSignUp] = useState(false);
  const [uniqueCheck, setUniqueCheck] = useState(undefined);

  const navigate = useNavigate();
  
  const handleUniqueCheck = (event) => {
      const username = event.target.value;
      if(username !== ""){
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Users/unique/" + username)
        .then((res) => setUniqueCheck(res.data));
      }else{
        setUniqueCheck(undefined);
      }
  }

  const handleSignUp = (data) => {
    const creds = {
      name: data['firstName'] + ' ' + data['lastName'],
      country: data['country'],
      dateOfBirth: data['dateOfBirth'],
      username: data['username'],
      password: data['password'],
    }

    GeneralAxiosService.postMethod("https://localhost:7067/api/v1/Users", creds)
    .then((res) => console.log(res.data))
    .then(() => setTimeout(() => {
      navigate("/");
    }, 3000))
    .catch((err) => {
      setSuccessfulSignUp(false);
    });
  };

  return (
    <section className='LoginSection PositionAboluteMiddle--dt container container--pa'>
        <Container className="container--styled container--pa" maxWidth="xs">
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
              Sign up
            </Typography>
            <Box component="form" noValidate onSubmit={handleSubmit(handleSignUp)} sx={{ mt: 3 }}>
              <Grid container spacing={2}>
                <Grid item xs={12} sm={6}>
                  <TextField
                    name="firstName"
                    required
                    fullWidth
                    id="firstName"
                    label="First Name"
                    autoFocus
                    error={!!errors['firstName']}
                    helperText={errors['firstName']?.message}
                    {...register('firstName', { required: true })}
                  />
                </Grid>
                
                <Grid item xs={12} sm={6}>
                  <TextField
                    name="lastName"
                    required
                    fullWidth
                    id="lastName"
                    label="Last Name"
                    autoFocus
                    error={!!errors['lastName']}
                    helperText={errors['lastName']?.message}
                    {...register('lastName', { required: true})}
                  />
                </Grid>

                <Grid item xs={12} sm={6}>
                  <TextField
                    required
                    fullWidth
                    id="country"
                    label="Country"
                    name="country"
                    error={!!errors['country']}
                    helperText={errors['country']?.message}
                    {...register('country', { required: true })}
                  />
                </Grid>

                <Grid item xs={12} sm={6}>
                <TextField
                  id="date"
                  label="Birthday"
                  type="date"
                  sx={{ width: '100%' }}
                  InputLabelProps={{
                    shrink: true,
                  }}
                  error={!!errors['dateOfBirth']}
                  helperText={errors['dateOfBirth']?.message}
                  {...register('dateOfBirth', { required: true })}
                />
                </Grid>

                <Grid item xs={12}>
                  <TextField
                    required
                    fullWidth
                    id="username"
                    label="Username"
                    name="username"
                    autoComplete='off'
                    error={!!errors['username']}
                    helperText={errors['username']?.message}
                    {...register('username', { required: true })}
                    onBlur={handleUniqueCheck}
                  />
                  {(uniqueCheck !== undefined && uniqueCheck) && <Alert severity="success"> Username entered is available!</Alert>}
                  {(uniqueCheck !== undefined && !uniqueCheck) && <Alert severity="error"> Username entered is not available!</Alert>}
                </Grid>

                <Grid item xs={12}>
                  <TextField
                    required
                    fullWidth
                    name="password"
                    label="Password"
                    type="password"
                    id="password"
                    autoComplete="new-password"
                    error={!!errors['password']}
                    helperText={errors['password']?.message}
                    {...register('password', { required: true, pattern:{
                      value: /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/,
                      message: "Password must be at least 8 characters long and must contain at least 1 capital letter, 1 number and 1 special character. "
                    }})}
                  />
                </Grid>

              </Grid>
              {successfulSignUp && <Alert severity="success" sx={{ width: '100%' }}>
                  Account created successfully
                </Alert>}
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
              >
                Sign Up
              </Button>
              <Grid container>
                <Link className="formLink" to="/login">
                    Already have an account? Sign in
                </Link>
              </Grid>
            </Box>
          </Box>
        </Container>
      </section>
  );
}