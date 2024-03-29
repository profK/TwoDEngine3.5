﻿module NewtonianObject

open TDE3ManagerInterfaces.GraphicsManagerInterface


type NewtonianObject = {x:float32; y:float32;r:float32;
                        vx:float32;vy:float32;vr:float32;
                        img:Image}

module NewtonianObject =
  
    let NewtonianUpdate  deltaMS (obj:NewtonianObject)  =
        let x = obj.x + obj.vx * (float32 deltaMS)
        let y = obj.y + obj.vy * (float32 deltaMS)
        let r = obj.r + obj.vr * (float32 deltaMS)
        {obj with x=x;y=y;r=r}
        
    let NewtonianUpdateList deltaMS (objList:NewtonianObject list) =
        objList
        |> List.map (fun obj -> NewtonianUpdate deltaMS obj)
        
    let Wrap (w:float32) (h:float32) (obj:NewtonianObject) =
        let x = if obj.x+obj.img.Size.X< 0.0f then w
                    else if obj.x > w then 0.0f-obj.img.Size.X else obj.x
        let y = if obj.y+obj.img.Size.Y < 0.0f then h
                    else if obj.y > h then 0.0f-obj.img.Size.Y else obj.y
        {obj with x=x;y=y}
        
    let WrapList (w:float32) (h:float32) (objList:NewtonianObject list) =
        objList
        |> List.map (fun obj ->
            let x = if obj.x+obj.img.Size.X< 0.0f then w
                    else if obj.x > w then 0.0f-obj.img.Size.X else obj.x
            let y = if obj.y+obj.img.Size.Y < 0.0f then h
                        else if obj.y > h then 0.0f-obj.img.Size.Y else obj.y
            if (x=obj.x)&&(y=obj.y) then
                obj
            else    
                {obj with x=x;y=y})
    let IsOffScreen(w:float32) (h:float32)  obj =
        (obj.x<0f)||(obj.x>=w)||(obj.y<0f)||(obj.y>=h)
        
    let RemoveOffScreen (w:float32) (h:float32) (objList:NewtonianObject list) =
        objList
        |> List.filter (fun obj ->
                not (IsOffScreen w h obj) 
            )