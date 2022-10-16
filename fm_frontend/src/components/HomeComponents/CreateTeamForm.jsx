import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import DialogTitle from '@mui/material/DialogTitle';
import { Button, TextField } from '@mui/material';
import { useEffect, useMemo, useState } from 'react';
import { useForm } from 'react-hook-form';
import countryList from 'react-select-country-list';
import GeneralAxiosService from "../../services/GeneralAxiosService";
import ListSelect from '../ListSelect';

const CreateTeamForm = (props) => {
    const defaults = {
        name: "",
        country: "",
        venue: "",
        leagueId: 0,
        logo: ""
      }
      
      const { register, handleSubmit, formState: { errors }, reset } = useForm(
        {
          defaultValues: defaults
        }
      );
  
      const countries = useMemo(() => countryList().getData().map(c => { 
        return {
          value: c.label,
          label: c.label
        };
      }), []);
  
      const [leagues, setLeagues] = useState([]);
  
      useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Leagues/All/0")
        .then((response) => setLeagues(response.data.pageResults.map(l => { 
            return {
              value: l.id,
              label: l.name
            };
          })));
      },[]);
  
      const formHandleClose = () => {
          props.handleClose();
      }
      
      const onSubmitHandle = (data) => {
        props.handleTeamCreate(data)
        formHandleClose()
        reset()
      }
  
      return <>
        <Dialog className='dialogContainer' open={props.open} onClose={formHandleClose} fullWidth>
        <DialogTitle>Create Team</DialogTitle>
        <DialogContent>
        <Box component="form" onSubmit={handleSubmit(onSubmitHandle)} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <TextField
                        required
                        autoComplete='off'
                        name="name"
                        fullWidth
                        label="Team Name"
                        error={!!errors['name']}
                        helperText={errors['name']?.message}
                        {...register('name', {pattern:{
                          value:  /^[A-Za-z\s]*$/,
                          message: "Team name must only contain alphabetic characters"
                        }})}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        required
                        autoComplete='off'
                        name="venue"
                        fullWidth
                        label="Venue Name"
                        error={!!errors['venue']}
                        helperText={errors['venue']?.message}
                        {...register('venue', {pattern:{
                          value:  /^[A-Za-z\s]*$/,
                          message: "Venue name must only contain alphabetic characters"
                        }})}
                    />
                </Grid>
  
                <Grid item xs={12} sm={6}>
                  <ListSelect
                  required
                  list={leagues} 
                  label="League"
                  formId="leagueId"
                  errors={errors} 
                  register={register}
                  />
                </Grid>
  
                <Grid item xs={12} sm={6}>  
                  <ListSelect 
                    required
                    list={countries} 
                    label="Country"
                    errors={errors} 
                    register={register}
                    />
                </Grid>
            </Grid>
            <DialogActions sx={{marginTop: "1.125rem"}}>
              <Button onClick={formHandleClose}>Cancel</Button>
              <Button type="submit">Create</Button>
            </DialogActions>
        </Box>
        </DialogContent>
        
      </Dialog>
      </>
}

export default CreateTeamForm;