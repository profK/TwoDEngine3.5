﻿module TDE3ManagerInterfaces.GraphicsManagerInterface

open System.Drawing
open System.IO
open System.Numerics

type SysColor = System.Drawing.Color
type SysRectangle = System.Drawing.Rectangle

    
type Transform =
    abstract Multiply : Vector2 -> Vector2
    abstract Multiply : Transform -> Transform
type VideoMode =
    | FullScreen
    | FullScreenWindow
    | Windowed of X:uint32 * Y:uint32
    

 [<AbstractClass>]
 type Window(graphicsManager:GraphicsManager)=
    member val graphics  = graphicsManager with get
    abstract Start : (Window -> unit) -> unit
    abstract Start : unit -> unit
    abstract Close : unit -> unit
    abstract Clear : SysColor -> unit
    abstract Show : unit -> unit
    abstract LoadImage : Stream -> Image
   
    abstract IdentityTransform : Transform with get
    abstract RotationTransform : float32 -> Transform
    abstract TranslationTransform : float32-> float32 -> Transform
    abstract ScaleTransform : float32-> float32 -> Transform
    abstract DrawImage : Transform->Image->unit
    abstract ScreenSize : Vector2 with get
    abstract IsOpen : unit -> bool
and GraphicsListener =
    abstract Update : GraphicsManager->uint -> string option
    abstract Render : GraphicsManager -> unit

and GraphicsManager =
    abstract OpenWindow : VideoMode->string->Window
    abstract GraphicsListeners : GraphicsListener list with get, set
   
    

and Image =
    abstract SubImage : Rectangle -> Image
    abstract Size : Vector2 with get
 