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
import GeneralAxiosService from "../services/GeneralAxiosService";
import ListSelect from './ListSelect';
import _ from 'lodash';

const PlayreFilterDialog = (props) => {
  const defaults = {
    name: "",
    minYearOfBirth: 0,
    maxYearOfBirth: 0,
    team: 0,
    country: "",
    position: ""
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

    const [teams, setTeams] = useState([]);

    useEffect(() => {
      GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Teams/List")
      .then((response) => setTeams(response.data.map(t => { 
          return {
            value: t.id,
            label: t.name
          };
        })));
    },[]);

    const dialogHandleClose = () => {
        props.handleClose();
    }
    
    const onSubmitHandle = async (data) => {
        if(!_.isEqual(data, defaults)){
          await props.handleFilterSubmit(data);
          props.handleFilterResetVisible();
        }

        reset();
        dialogHandleClose();
    }

    return <>
        <Dialog className='dialogContainer' open={props.openFilter} onClose={dialogHandleClose} fullWidth>
      <DialogTitle>Filter Players list</DialogTitle>
      <DialogContent>
      <Box component="form" onSubmit={handleSubmit(onSubmitHandle)} sx={{ mt: 3 }}>
          <Grid container spacing={2}>
              <Grid item xs={12}>
                  <TextField
                      name="name"
                      fullWidth
                      label="Player Name"
                      error={!!errors['name']}
                      helperText={errors['name']?.message}
                      {...register('name', {pattern:{
                        value:  /^[A-Za-z\s]*$/,
                        message: "Player name must only contain alphabetic characters"
                      }})}
                  />
              </Grid>

              {props.team && <Grid item xs={12}>
                <ListSelect 
                NoneSelect
                list={teams} 
                label="Team"
                errors={errors} 
                register={register}
                />
              </Grid>}

              <Grid item xs={12} sm={6}>  
                <ListSelect 
                  NoneSelect
                  list={countries} 
                  label="Country"
                  errors={errors} 
                  register={register}
                  />
              </Grid>

              <Grid item xs={12} sm={6}>  
                <ListSelect 
                  NoneSelect
                  list={[
                    { value: "Attacker", label: "Attacker" },
                    { value: "Midfielder", label: "Midfielder" },
                    { value: "Defender", label: "Defender" },
                    { value: "Goalkeeper", label: "Goalkeeper" }
                  ]} 
                  label="Position"
                  errors={errors} 
                  register={register}
                />
              </Grid>

              <Grid item xs={6} sm={6}>  
                  <TextField
                      label="Min age"
                      type="number"
                      error={!!errors['minYearOfBirth']}
                      helperText={errors['minYearOfBirth']?.message}
                      {...register('minYearOfBirth')}
                  />
              </Grid>
              
              <Grid item xs={6} sm={6}>  
                  <TextField
                      label="Max age"
                      type="number"
                      error={!!errors['maxYearOfBirth']}
                      helperText={errors['maxYearOfBirth']?.message}
                      {...register('maxYearOfBirth')}
                  />
              </Grid>
          </Grid>
          <DialogActions sx={{marginTop: "1.125rem"}}>
            <Button onClick={dialogHandleClose}>Cancel</Button>
            <Button type="submit">Apply</Button>
          </DialogActions>
      </Box>
      </DialogContent>
      
    </Dialog>
    </>
}

export default PlayreFilterDialog;