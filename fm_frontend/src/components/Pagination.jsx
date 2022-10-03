import IconButton from '@mui/material/IconButton';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';

const Pagination = (props) => {
    const flag = props.gameweekPagination;
    return <div className={flag ? 'gameweekPagination flex flex-ai-c flex-jc-sb': 'flex flex-ai-c'}>
        <IconButton 
            aria-label="backwardPage"
            sx={{borderRadius: '0'}}
            onClick={() => {
                if(!props.pageLoading){
                    if(props.page > 1){
                        props.handlePageChange(parseInt(props.page) - 1)
                    }
                }
            }}
            disabled={props.page === 1}>
            <ChevronLeftIcon />
        </IconButton>
        
        <p>{flag && "Gameweek "}{props.page}</p>

        <IconButton 
            aria-label="forwardPage" 
            sx={{borderRadius: '0'}}
            onClick={() => {
                if(!props.pageLoading){
                    if(props.page < props.maxPages){
                        props.handlePageChange(props.page + 1)
                    }  
                }
            }}
            disabled={props.page === props.maxPages}
            >
            <ChevronRightIcon />
        </IconButton>
    </div>
}

export default Pagination;