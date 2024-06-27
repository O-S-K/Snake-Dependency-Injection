# Dependency Injection in Unity Snake Game

## Overview

This document provides an overview of how Dependency Injection (DI) is implemented in the Unity Snake Game project. Dependency Injection is used to manage dependencies between different game components, allowing for better modularity, testability, and flexibility in the codebase.

## What is Dependency Injection?

Dependency Injection is a design pattern widely used in software development to achieve Inversion of Control (IoC). In the context of this Unity project, DI helps decouple components and services by injecting dependencies rather than having components create their dependencies directly.

## Components of the Project

### DIContainer Class

The `DIContainer` class serves as a simple Dependency Injection container in this project. It allows bindings between interfaces and their concrete implementations or instances.

#### Methods

- **Bind<TInterface, TImplementation>()**: This is the method to use when you want DIContainer to know how to create an object when needed.
- **ProvideInstance<TInterface>(instance)**: This is the method to use when you want to provide a concrete instance of TInterface without a concrete implementation class.
- **Resolve**: This is the method to use when you need to get an instance of the TInterface linked in the DIContainer.
- **Inject(target)**: Injects dependencies into an object `target` by setting its fields annotated with `[Inject]`.

### Using DI in the Snake Game
Dependencies are bound to their interfaces and provided to the `DIContainer`. This happens typically during initialization, such as in the `Awake()` method of `GameInstaller`.

Example:
```csharp

Class GameInstaller

// Instantiate objects in the scene
   var dataSO = Resources.Load<DataSO>("Data/DataSO"); 
   var gameControllerPrefab = dataSO.GameController;
   var gridPrefab = dataSO.GridPrefab;
   var snakePrefab = dataSO.SnakePrefab;

// Bind dependencies to DIContainer
   DIContainer.Bind<IDataSO>(() => dataSO);
   DIContainer.Bind<IGameController>(() => gameController);
   DIContainer.Bind<ISnake>(() => snake);
   DIContainer.Bind<IGrid>(() => grid);


 // Inject dependencies into instantiated objects
    DIContainer.Inject(dataSO);
    DIContainer.Inject(game);
    DIContainer.Inject(snake);
    DIContainer.Inject(grid);

Class Snake
 // Injected
    [Inject] private IDataSO dataSo;
    [Inject] private IGameController gameController;
    [Inject] private IGrid grid;
    [Inject] private IInput input;


