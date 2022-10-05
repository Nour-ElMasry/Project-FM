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

const PlayreFilterDialog = (props) => {
    const { register, handleSubmit, formState: { errors }, reset } = useForm(
      {
        defaultValues: {
          name: " ",
          minYearOfBirth: 0,
          maxYearOfBirth: 0,
          team: 0,
          country: " ",
          position: " "
        }
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
        await props.handleFilterSubmit(data);
        sessionStorage.setItem("PlayersFilter_Key", JSON.stringify(data))
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
                list={teams} 
                label="Team"
                errors={errors} 
                register={register}
                />
              </Grid>}

              <Grid item xs={12} sm={6}>  
                <ListSelect 
                  list={countries} 
                  label="Country"
                  errors={errors} 
                  register={register}
                  />
              </Grid>

              <Grid item xs={12} sm={6}>  
                <ListSelect 
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
                      value={0}
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
                      value={0}
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